using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class Expense
{
    public int ExpensesId { get; set; }

    public string ExpenseType { get; set; } = null!;

    public float ExpenseAmmount { get; set; }

    public DateOnly Date { get; set; }
}
