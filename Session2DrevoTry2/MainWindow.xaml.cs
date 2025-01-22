using Session2DrevoTry2.DB;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Session2DrevoTry2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<int,List<Button>> buttonList = new Dictionary<int, List<Button>>();

        public MainWindow()
        {
            InitializeComponent();
            List<Subdivision> subdivisions = DBInstance.GetInstance().Subdivisions.ToList();
            GetStackPanelLevel(subdivisions.Where(s => s.SubdivisionNavigation == null).ToList(), 1);
            SetCanvasOffset();
            GetWidthAndHeightCanvas();
            DrawLines();
        }

        private async void DrawLines()
        {
            await DrawLinesAsync();
        }

        private async Task DrawLinesAsync()
        {
            await Task.Delay(750);
            for (int i = buttonList.Count; i > 1; i--)
            {
                foreach (Button lastButton in buttonList[i])
                {
                    foreach (Button nextButton in buttonList[i - 1])
                    {
                        if ((lastButton.Tag as Subdivision).SubdivisionNavigation != nextButton.Tag as Subdivision)
                            continue;
                        double lastButtonX = Canvas.GetLeft(lastButton);
                        double lastButtonY = Canvas.GetTop(lastButton);
                        double nextButtonX = Canvas.GetLeft(nextButton);
                        double nextButtonY = Canvas.GetTop(nextButton);
                        Line line = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = lastButtonX + 10 + lastButton.ActualWidth / 2,
                            Y1 = lastButtonY + 10,
                            X2 = nextButtonX + 10 + nextButton.ActualWidth / 2,
                            Y2 = nextButtonY + nextButton.ActualHeight + 10
                        };

                        Line arrow1 = new Line
                        {
                            Stroke = Brushes.Red,
                            X1 = lastButtonX + 10 + lastButton.ActualWidth / 2,
                            Y1 = lastButtonY + 10
                        };
                        Line arrow2 = new Line
                        {
                            Stroke = Brushes.Red,
                            X1 = lastButtonX + 10 + lastButton.ActualWidth / 2,
                            Y1 = lastButtonY + 10
                        };
                        if (lastButtonX < nextButtonX)
                        {
                            arrow1.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 + 30;
                            arrow1.Y2 = lastButtonY - 10;
                            arrow2.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 + 40;
                            arrow2.Y2 = lastButtonY + 9;
                        }
                        else if (lastButtonX > nextButtonX)
                        {
                            arrow1.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 - 30;
                            arrow1.Y2 = lastButtonY - 10;
                            arrow2.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 - 40;
                            arrow2.Y2 = lastButtonY + 9;
                        }
                        else
                        {
                            arrow1.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 + 20;
                            arrow1.Y2 = lastButtonY - 10;
                            arrow2.X2 = lastButtonX + 10 + lastButton.ActualWidth / 2 - 20;
                            arrow2.Y2 = lastButtonY - 10;
                        }
                        Panel.SetZIndex(line, -1);
                        mainCanvas.Children.Add(line);
                        mainCanvas.Children.Add(arrow1);
                        mainCanvas.Children.Add(arrow2);

                    }
                }
            }
        }

        private async void GetWidthAndHeightCanvas()
        {
            await SetWidthAndHeightCanvas();
        }

        private async Task SetWidthAndHeightCanvas()
        {
            await Task.Delay(500);
            for (int i = 1; i < buttonList.Count; i++)
            {
                Button lastButton = buttonList[i].Last();
                double widht = Canvas.GetLeft(lastButton) + lastButton.ActualWidth + 20;
                if (widht > mainCanvas.ActualWidth)
                    mainCanvas.Width = widht;
            }
            var height = buttonList.Count * 120;
            mainCanvas.Height = height;
        }

        private async void SetCanvasOffset()
        {
            for (int level = 0; level < buttonList.Count; level++)
            {
                await SetButtonCanvasOffsetAsync(buttonList[level + 1], level);
            }
        }

        private void GetStackPanelLevel(List<Subdivision> subdivisions, int level)
        {
            if (buttonList.Count < level)
                buttonList.Add(level, new List<Button>());
            foreach (Subdivision subdivision in subdivisions)
            {
                GenerateBlocks(subdivision, buttonList[level]);
                if (subdivision.InverseSubdivisionNavigation.Count > 0)
                    GetStackPanelLevel(subdivision.InverseSubdivisionNavigation.ToList(), level + 1);
            }
        }

        private void GenerateBlocks(Subdivision subdivision, List<Button> buttons)
        {
            Button button = new Button { Content = subdivision.Name, Margin = new Thickness(10), Padding = new Thickness(0,10,0,10), Tag = subdivision };
            mainCanvas.Children.Add(button);
            buttons.Add(button);
        }

        private async Task SetButtonCanvasOffsetAsync(List<Button> buttons, int level)
        {
            await Task.Delay(5);
            for (int i = 0; i < buttons.Count; i++)
            {
                Canvas.SetTop(buttons[i], 120 * level);
                double leftOffset = 0;
                if (i > 0)
                {
                    Button prevButton = buttons[i - 1];
                    leftOffset = Canvas.GetLeft(prevButton) + prevButton.ActualWidth + 10;
                }
                Canvas.SetLeft(buttons[i], leftOffset);
            }
        }
    }
}