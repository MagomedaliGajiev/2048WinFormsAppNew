using System.Text;

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
        private static string _historyFilePath = "game_history.txt";
        private string _currentPlayer = "Player";

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
            try
            {
                string record = $"{_currentPlayer}|{_score}|{DateTime.Now:yyyy-MM-dd HH:mm}\n";
                File.AppendAllText(_historyFilePath, record);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения истории: {ex.Message}");
            }
        }

        private void ShowGameHistory()
        {
            if (!File.Exists(_historyFilePath))
            {
                MessageBox.Show("История игр пуста!", "История");
                return;
            }

            try
            {
                var history = new StringBuilder();
                history.AppendLine("История игр:\n");
                history.AppendLine("Игрок\t\tОчки\t\tДата");
                history.AppendLine("-----------------------------------");

                foreach (var line in File.ReadAllLines(_historyFilePath))
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 3)
                    {
                        history.AppendLine($"{parts[0]}\t\t{parts[1]}\t\t{parts[2]}");
                    }
                }

                MessageBox.Show(history.ToString(), "История игр");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения истории: {ex.Message}");
            }
        }

        private void AskPlayerName()
        {
            using (var inputForm = new InputForm("Введите ваше имя:", _currentPlayer))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    _currentPlayer = string.IsNullOrWhiteSpace(inputForm.InputText)
                        ? "Player"
                        : inputForm.InputText;
                }
            }
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
                    // Проверка по горизонтали
                    if (j < _mapSize - 1 && _labelsMap[i, j].Text == _labelsMap[i, j + 1].Text)
                        return true;

                    // Проверка по вертикали
                    if (i < _mapSize - 1 && _labelsMap[i, j].Text == _labelsMap[i + 1, j].Text)
                        return true;
                }
            }

            return false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitMap();
            ResetGame();
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
            int x = 10 + IndexColumn * 76;
            int y = 90 + indexRow * 76;
            label.Location = new Point(x, y);

            return label;
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
            int value = _random.Next(100) < 75 ? 2 : 4;

            _labelsMap[randomCell.row, randomCell.col].Text = value.ToString();
        }

        private void ResetGame()
        {
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

            // Запрос имени игрока
            AskPlayerName();
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
            }

            // Проверка окончания игры
            if (!HasMoves())
            {
                SaveGameResult();
                if (MessageBox.Show("Игра окончена! Хотите сыграть еще?", "Конец игры",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    Close();
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
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