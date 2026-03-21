using System.Windows.Controls;
using MoneyManager.Models;

namespace MoneyManager.WpfUI
{
    public partial class IndividualTransaction : Page
    {
        public IndividualTransaction(TransactionDetailsDto transaction)
        {
            InitializeComponent();
            this.DataContext = transaction;
        }
    }
}