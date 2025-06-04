namespace _2048WinFormsAppNew
{
    // i * mapSize + j = number
    public partial class MainForm : Form
    {
        private const int _mapSize = 4;
        private Label[,] _labelsMap;
        private static Random _random = new Random();
        private int _score = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitMap();
            GenerateNumber();
            ShowScore();
        }

        private void ShowScore()
        {
            scoreLabel.Text = _score.ToString();
        }

        private void InitMap()
        {
            _labelsMap = new Label[_mapSize, _mapSize];

            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    _labelsMap[i, j] = newLabel;
                }
            }

        }

        private Label CreateLabel(int indexRow, int IndexColumn)
        {
            var label = new Label();
            label.BackColor = SystemColors.ButtonShadow;
            label.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label.Size = new Size(70, 70);
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = 10 + IndexColumn * 76; // 10 + j * (140 +6)
            int y = 70 + indexRow * 76;   // 140 + j * (140 +6)
            label.Location = new Point(x, y);

            return label;
        }

        private void GenerateNumber()
        {
            while (true)
            {
                var randomNumberLabel = _random.Next(_mapSize * _mapSize);
                var indexRow = randomNumberLabel / _mapSize;
                var indexColumn = randomNumberLabel % _mapSize;

                if (_labelsMap[indexRow, indexColumn].Text == string.Empty)
                {
                    // нужно рандомно сгенирировать либо 2 либо 4
                    _labelsMap[indexRow, indexColumn].Text = "2";
                    break;
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < _mapSize; i++)
                {
                    for (int j = _mapSize - 1; j >= 0; j--)
                    {
                        if (_labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (_labelsMap[i, k].Text != string.Empty)
                                {
                                    if (_labelsMap[i, j].Text == _labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(_labelsMap[i, j].Text);
                                        _score += number * 2;
                                        _labelsMap[i, j].Text = (number * 2).ToString();
                                        _labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < _mapSize; i++)
                {
                    for (int j = _mapSize - 1; j >= 0; j--)
                    {
                        if (_labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (_labelsMap[i, k].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[i, k].Text;
                                    _labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < _mapSize; i++)
                {
                    for (int j = 0; j < _mapSize; j++)
                    {
                        if (_labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[i, k].Text != string.Empty)
                                {
                                    if (_labelsMap[i, j].Text == _labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(_labelsMap[i, j].Text);
                                        _score += number * 2;
                                        _labelsMap[i, j].Text = (number * 2).ToString();
                                        _labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < _mapSize; i++)
                {
                    for (int j = 0; j < _mapSize; j++)
                    {
                        if (_labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[i, k].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[i, k].Text;
                                    _labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    for (int i = 0; i < _mapSize; i++)
                    {
                        if (_labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[k, j].Text != string.Empty)
                                {
                                    if (_labelsMap[i, j].Text == _labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(_labelsMap[i, j].Text);
                                        _score += number * 2;
                                        _labelsMap[i, j].Text = (number * 2).ToString();
                                        _labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int j = 0; j < _mapSize; j++)
                {
                    for (int i = 0; i < _mapSize; i++)
                    {
                        if (_labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[k, j].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[k, j].Text;
                                    _labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    for (int i = _mapSize - 1; i >= 0; i--)
                    {
                        if (_labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (_labelsMap[k, j].Text != string.Empty)
                                {
                                    if (_labelsMap[i, j].Text == _labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(_labelsMap[i, j].Text);
                                        _score += number * 2;
                                        _labelsMap[i, j].Text = (number * 2).ToString();
                                        _labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int j = 0; j < _mapSize; j++)
                {
                    for (int i = _mapSize - 1; i >= 0; i--)
                    {
                        if (_labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (_labelsMap[k, j].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[k, j].Text;
                                    _labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            GenerateNumber();
            ShowScore();
        }
    }
}
