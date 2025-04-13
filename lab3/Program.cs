using System;

public abstract class Employee
{
    public string Name { get; set; }
    public DateTime EmploymentDate { get; set; }
    public decimal Rate { get; set; }

    public Employee(string name, DateTime employmentDate, decimal rate)
    {
        Name = name;
        EmploymentDate = employmentDate;
        Rate = rate;
    }

    public abstract void GetInfo();
    public abstract decimal GetPrice();
}

public class KitchenWorker : Employee
{
    public int HoursWorked { get; set; }

    public KitchenWorker(string name, DateTime employmentDate, decimal rate, int hoursWorked) : base(name, employmentDate, rate)
    {
        HoursWorked = hoursWorked;
    }

    public override void GetInfo()
    {
        Console.WriteLine($"Работник кухни: {Name}, Дата трудоустройства: {EmploymentDate.ToShortDateString()}, Ставка: {Rate}, Отработано часов: {HoursWorked}");
    }

    public override decimal GetPrice()
    {
        return Rate * HoursWorked;
    }
}

public class Waiter : Employee
{
    public int HoursWorked { get; set; }
    public decimal Tips { get; set; }

    public Waiter(string name, DateTime employmentDate, decimal rate, int hoursWorked, decimal tips) : base(name, employmentDate, rate)
    {
        HoursWorked = hoursWorked;
        Tips = tips;
    }

    public override void GetInfo()
    {
        Console.WriteLine($"Официант: {Name}, Дата трудоустройства: {EmploymentDate.ToShortDateString()}, Ставка: {Rate}, Отработано часов: {HoursWorked}, Чаевые: {Tips}");
    }

    public override decimal GetPrice()
    {
        return Rate * HoursWorked + Tips;
    }
}

public class Manager : Employee
{
    public decimal Bonus { get; set; }

    public Manager(string name, DateTime employmentDate, decimal rate, decimal bonus) : base(name, employmentDate, rate)
    {
        Bonus = bonus;
    }

    public override void GetInfo()
    {
        Console.WriteLine($"Менеджер: {Name}, Дата трудоустройства: {EmploymentDate.ToShortDateString()}, Ставка: {Rate}, Премия: {Bonus}");
    }

    public override decimal GetPrice()
    {
        int yearsWorked = DateTime.Now.Year - EmploymentDate.Year;
        return Rate + Bonus * yearsWorked; // ЗП менеджера
    }
}

public class JuniorManager : Manager
{
    public JuniorManager(string name, DateTime employmentDate, decimal rate, decimal bonus) : base(name, employmentDate, rate, bonus) { }

    public override decimal GetPrice()
    {
        int yearsWorked = DateTime.Now.Year - EmploymentDate.Year;
        decimal baseSalary = Rate;

        if (DateTime.Now.Month == 6 || DateTime.Now.Month == 12)
        {
            baseSalary += Bonus * yearsWorked; // Премия только в июне и декабре
        }
        return baseSalary;
    }
    public override void GetInfo()
    {
        Console.WriteLine($"Младший менеджер: {Name}, Дата трудоустройства: {EmploymentDate.ToShortDateString()}, Ставка: {Rate}, Премия: {Bonus}");
    }
}


public class Program
{
    public static void Main(string[] args)
    {


        KitchenWorker kitchenWorker1 = new KitchenWorker("Повар1", new DateTime(2023, 1, 15), 100, 160);
        KitchenWorker kitchenWorker2 = new KitchenWorker("Повар2", new DateTime(2023, 2, 20), 110, 170);
        Waiter waiter1 = new Waiter("Официант1", new DateTime(2023, 3, 10), 80, 150, 5000);
        Waiter waiter2 = new Waiter("Официант2", new DateTime(2022, 4, 5), 90, 160, 6000);
        Manager manager = new Manager("Менеджер", new DateTime(2020, 5, 2), 20000, 10000);
        JuniorManager juniorManager = new JuniorManager("Младший менеджер", new DateTime(2021, 6, 8), 18000, 8000);

        List<Employee> employees = new List<Employee>() { kitchenWorker1, kitchenWorker2, waiter1, waiter2, manager, juniorManager };

        foreach (var employee in employees)
        {
            employee.GetInfo();
            Console.WriteLine($"ЗП: {employee.GetPrice()}");
            Console.WriteLine();
        }


    }
}