namespace _2048WinFormsAppNew
{
    public partial class MainForm : Form
    {
        private int _mapSize = 4; // По умолчанию 4x4
        private static string _settingsFilePath = "settings.dat";
        private Label[,] _labelsMap;
        private static Random _random = new Random();
        private int _score = 0;
        private int _bestScore = 0;
        private string _currentPlayer;
        private InputForm _inputForm;

        public MainForm()
        {
            InitializeComponent();
            LoadSettings(); // Загружаем размер поля
            LoadBestScore();
        }

        private void LoadBestScore()
        {
            if (File.Exists("best_score.dat"))
            {
                try
                {
                    _bestScore = int.Parse(File.ReadAllText("best_score.dat"));
                    bestScoreLabel.Text = _bestScore.ToString();
                }
                catch
                {
                    _bestScore = 0;
                }
            }
        }

        private void SaveBestScore()
        {
            if (_score > _bestScore)
            {
                _bestScore = _score;
                bestScoreLabel.Text = _bestScore.ToString();
                File.WriteAllText("best_score.dat", _bestScore.ToString());
            }
        }

        private void SaveGameResult()
        {
            UserManager.Add(new User() { Name = _currentPlayer, Score = _score, DateTime = DateTime.Now} );
        }

        private void ShowGameHistory()
        {
            var gameHistory = new GameHistoryForm();
            gameHistory.ShowDialog();
        }

        private bool HasMoves()
        {
            // Проверка пустых клеток
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (string.IsNullOrEmpty(_labelsMap[i, j].Text))
                        return true;
                }
            }

            // Проверка возможных слияний
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (j < _mapSize - 1 && _labelsMap[i, j].Text == _labelsMap[i, j + 1].Text)
                        return true;

                    if (i < _mapSize - 1 && _labelsMap[i, j].Text == _labelsMap[i + 1, j].Text)
                        return true;
                }
            }

            return false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitMap();
            StartGame();
        }

        private void ShowScore()
        {
            scoreLabel.Text = _score.ToString();
        }

        private void InitMap()
        {
            // Удаляем старые метки
            if (_labelsMap != null)
            {
                foreach (var label in _labelsMap)
                {
                    Controls.Remove(label);
                }
            }

            _labelsMap = new Label[_mapSize, _mapSize];

            // Рассчитываем размер ячейки в зависимости от размера поля
            var cellSize = _mapSize > 5 ? 60 : 70;
            var spacing = 6;
            var startX = 10;
            var startY = 90;

            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j, cellSize);
                    // Устанавливаем начальные цвета
                    newLabel.BackColor = SystemColors.ButtonShadow;
                    newLabel.ForeColor = SystemColors.ControlText;

                    newLabel.Location = new Point(
                        startX + j * (cellSize + spacing),
                        startY + i * (cellSize + spacing));
                    Controls.Add(newLabel);
                    _labelsMap[i, j] = newLabel;
                }
            }

            // Обновляем размер формы
            var formWidth = startX * 2 + _mapSize * (cellSize + spacing);
            var formHeight = startY + 50 + _mapSize * (cellSize + spacing);
            ClientSize = new Size(formWidth, formHeight);
        }


        private Label CreateLabel(int indexRow, int IndexColumn, int size)
        {
            return new Label()
            {
                BackColor = SystemColors.ButtonShadow,
                Font = new Font("Segoe UI", GetFontSize(size), FontStyle.Bold),
                Size = new Size(size, size),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private float GetFontSize(int cellSize)
        {
            return cellSize > 60 ? 18f : 14f;
        }

        private void GenerateNumber()
        {
            List<(int row, int col)> emptyCells = new List<(int, int)>();

            // Сбор списка пустых ячеек
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (string.IsNullOrEmpty(_labelsMap[i, j].Text))
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            if (emptyCells.Count == 0)
                return;

            // Выбор случайной пустой ячейки
            var randomCell = emptyCells[_random.Next(emptyCells.Count)];

            // Генерация 2 (75%) или 4 (25%)
            var value = _random.Next(100) < 75 ? 2 : 4;

            _labelsMap[randomCell.row, randomCell.col].Text = value.ToString();
            // Цвет обновится в UpdateTileColors
        }

        private void StartGame()
        {
            _inputForm = new InputForm();
            _currentPlayer = _inputForm.inputTextBox.Text;
            _inputForm.ShowDialog();
            _currentPlayer = _inputForm.inputTextBox.Text;
            _score = 0;
            ShowScore();

            // Очистка игрового поля
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    _labelsMap[i, j].Text = string.Empty;
                }
            }

            // Генерация двух начальных чисел
            GenerateNumber();
            GenerateNumber();
            UpdateTileColors(); // Обновляем цвета после сброса
        }

        private Color GetTileColor(int value)
        {
            switch (value)
            {
                case 2: return Color.FromArgb(238, 228, 218); // #eee4da
                case 4: return Color.FromArgb(237, 224, 200); // #ede0c8
                case 8: return Color.FromArgb(242, 177, 121); // #f2b179
                case 16: return Color.FromArgb(245, 149, 99); // #f59563
                case 32: return Color.FromArgb(246, 124, 95); // #f67c5f
                case 64: return Color.FromArgb(246, 94, 59); // #f65e3b
                case 128: return Color.FromArgb(237, 207, 114); // #edcf72
                case 256: return Color.FromArgb(237, 204, 97); // #edcc61
                case 512: return Color.FromArgb(237, 200, 80); // #edc850
                case 1024: return Color.FromArgb(237, 197, 63); // #edc53f
                case 2048: return Color.FromArgb(237, 194, 46); // #edc22e
                default: return Color.FromArgb(60, 58, 50);    // Для больших значений
            }
        }

        private void UpdateTileColors()
        {
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    var label = _labelsMap[i, j];
                    if (!string.IsNullOrEmpty(label.Text) && int.TryParse(label.Text, out int value))
                    {
                        label.BackColor = GetTileColor(value);
                        // Устанавливаем контрастный цвет текста
                        label.ForeColor = value < 8 ? Color.FromArgb(119, 110, 101) : Color.White;
                    }
                    else
                    {
                        label.BackColor = SystemColors.ButtonShadow;
                        label.ForeColor = SystemColors.ControlText;
                    }
                }
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(_settingsFilePath))
            {
                try
                {
                    string[] settings = File.ReadAllText(_settingsFilePath).Split('|');
                    if (settings.Length >= 1)
                    {
                        _mapSize = int.Parse(settings[0]);
                    }
                }
                catch
                {
                    _mapSize = 4;
                }
            }
        }

        private void SaveSettings()
        {
            File.WriteAllText(_settingsFilePath, $"{_mapSize}");
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool moved = false;

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
                                        moved = true;
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
                                    moved = true;
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
                                        moved = true;
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
                            for (int k = j + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[i, k].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[i, k].Text;
                                    _labelsMap[i, k].Text = string.Empty;
                                    moved = true;
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
                                        moved = true;
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
                            for (int k = i + 1; k < _mapSize; k++)
                            {
                                if (_labelsMap[k, j].Text != string.Empty)
                                {
                                    _labelsMap[i, j].Text = _labelsMap[k, j].Text;
                                    _labelsMap[k, j].Text = string.Empty;
                                    moved = true;
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
                                        moved = true;
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
                                    moved = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (moved)
            {
                GenerateNumber();
                ShowScore();
                SaveBestScore();
                UpdateTileColors(); // Обновляем цвета после перемещения
            }

            // Проверка окончания игры
            if (!HasMoves())
            {
                SaveGameResult();
                if (MessageBox.Show("Игра окончена, к сожалению вы проиграли! Хотите сыграть еще?", "Конец игры",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    StartGame();
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Правила игры 2048:\n\n" +
                "1. Используйте стрелки клавиатуры для перемещения плиток\n" +
                "2. При столкновении одинаковых плиток они объединяются\n" +
                "3. После каждого хода появляется новая плитка (2 или 4)\n" +
                "4. Цель - получить плитку 2048\n\n" +
                "Управление:\n← → ↑ ↓ - движение плиток",
                "Правила игры"
            );
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGameHistory();
        }
    }
}