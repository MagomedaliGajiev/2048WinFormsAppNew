
namespace _2048WinFormsAppNew
{
    public partial class GameHistoryForm : Form
    {
        public GameHistoryForm()
        {
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            var gameHistory = UserManager.GetALl()
                .Select(h => new
                {
                    Имя = h.Name,
                    Очки = h.Score,
                    Дата = h.DateTime.ToString("dd.MM.yyyy HH:mm")
                }).ToList();

            resultsDataGridView.DataSource = gameHistory;
            resultsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
