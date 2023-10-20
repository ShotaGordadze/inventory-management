
using OwlTrading.InventoryManagement.Domain.General;
using OwlTrading.InventoryManagement.Domain.OrderManagement;
using OwlTrading.InventoryManagement.Domain.ProductManagement;

namespace OwlTrading.InventoryManagement;

internal static class Utilities
{
    private static List<Product> Inventory = new();
    private static List<Order> Orders = new();

    private static void FillInventory()
    {
        Inventory.Add(new Product(1, "Khvanchkara", " ", ItemType.Wine, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 10
        }, 100)
        { AmountInStock = 150 });

        Inventory.Add(new Product(2, "Rqatsiteli", " ", ItemType.Wine, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 15
        }, 100)
        { AmountInStock = 50 });

        Inventory.Add(new Product(3, "Mitsis tkhili", " ", ItemType.Nuts, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 5.80
        }, 100)
        { AmountInStock = 150 });

        Inventory.Add(new Product(4, "Nigozi", " ", ItemType.Nuts, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 7
        }, 100)
        { AmountInStock = 50 });

        Inventory.Add(new Product(5, "Kaxuri atami", " ", ItemType.Peach, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 3.5
        }, 100)
        { AmountInStock = 150 });

        Inventory.Add(new Product(6, "Rachuli atami", " ", ItemType.Peach, new()
        {
            Currency = Currency.Lari,
            ItemPrice = 4
        }, 100)
        { AmountInStock = 50 });
    }

    public static void ShowMainMenu()
    {
        FillInventory();

        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("********************");
        Console.WriteLine("* Select an action *");
        Console.WriteLine("********************");

        Console.WriteLine("1: Inventory management");
        Console.WriteLine("2: Order management");
        Console.WriteLine("3: Settings");
        Console.WriteLine("4: Save all data");
        Console.WriteLine("0: Close application");

        Console.WriteLine("Your selection: ");

        int userSelection;

        while (true)
        {
            try
            {
                userSelection = int.Parse(Console.ReadLine());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} Try again");
            }
        }
        switch (userSelection)
        {
            case 1:
                ShowInventoryManagementMenu();
                break;
            case 2:
                ShowOrderManagementMenu();
                break;
            case 3:
                ShowSettingsMenu();
                break;
            case 4:
                //SaveAllData();
                break;
            case 5:
                break;

        }
    }

    private static void ShowInventoryManagementMenu()
    {
        string? userSelection;
        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Inventory management *");
            Console.WriteLine("************************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What you want to do:");
            Console.ResetColor();
            Console.WriteLine("1: View details of product (ID)");
            Console.WriteLine("2: View details of product (Name)");
            Console.WriteLine("3: View products with low stock (ID)");
            Console.WriteLine("0: Back to main menu");

            Console.WriteLine("Your selection: ");
            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowProductDetailId();
                    break;

                case "2":
                    ShowProductDetailName();
                    break;

                case "3":
                    ShowLowStockProduct();
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }

    private static void ShowOrderManagementMenu()
    {
        string? userSelection;
        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Order management *");
            Console.WriteLine("************************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What you want to do: ");
            Console.WriteLine("1: Open order overview");
            Console.WriteLine("2: Add new order");
            Console.WriteLine("0: Back to main menu");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    OpenOrderOverview(); //ToDo
                    break;

                case "2":
                    AddOrder(); //ToDo
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }

    private static void ShowSettingsMenu()
    {
        string? userSelection;

        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("************");
            Console.WriteLine("* Settings *");
            Console.WriteLine("************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do?");
            Console.ResetColor();

            Console.WriteLine("1: Change stock threshold (ID)");
            Console.WriteLine("0: Back to main menu");

            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ChangeStockThreshold(); //ToDo
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }

    #region ShowInventoryManagementMenu methods

    private static void ShowProductDetailId()
    {
        string? selectedProductId;
        Product? selectedProduct;

        Console.WriteLine("Enter the ID of product: ");
        {
            while (true)
            {
                try
                {
                    selectedProductId = Console.ReadLine();

                    selectedProduct = Inventory.FirstOrDefault(p => p.Id == int.Parse(selectedProductId));
                    break;
                }
                catch (ArgumentNullException anex)
                {
                    Console.WriteLine($"{anex.Message}, please enter correct ID ");
                }

                catch (FormatException fex)
                {
                    Console.WriteLine($"{fex.Message}, please enter correct ID ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}, please enter correct ID ");
                }
            }

            if (selectedProduct != null)
            {
                Console.WriteLine(selectedProduct.DisplayFullDetails());
                Console.ReadLine();
                //ToDo User might need to do some operations here
            }
            else
            {
                Console.WriteLine("Non-existing product selected. Please try again.");
                Console.ReadLine();
            }
        }
    }

    private static void ShowProductDetailName()
    {
        string? selectedProductName;

        Console.WriteLine("Enter the Name of product: ");
        selectedProductName = Console.ReadLine();

        if (selectedProductName != null)
        {
            Product? selectedProduct = Inventory.Where(p => p.Name.ToUpper().Equals(selectedProductName.ToUpper())).FirstOrDefault();
            if (selectedProduct != null)
            {
                Console.WriteLine(selectedProduct.DisplayFullDetails());
                Console.ReadLine();

                //ToDo User might need to do some operations here
            }
            else
            {
                Console.WriteLine("Non-existing product selected. Please try again.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Non-existing product selected. Please try again.");
            Console.ReadLine();
        }
    }

    private static void ShowLowStockProduct()
    {
        foreach (var product in Inventory)
        {
            if (product != null && product.IsBelowStockThreshold == true)
            {
                Console.WriteLine(product.DisplayShortDetails());
            }
        }

        Console.ReadLine();
    }

    #endregion

    #region ShowOrderManagementMenu methods

    private static void OpenOrderOverview()
    {
        throw new NotImplementedException();
    }

    private static void AddOrder()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region ShowSettingsMenu methods
    private static void ChangeStockThreshold()
    {
        int userSelectionId;
        decimal selectedStockThreshold;
        Console.WriteLine("Enter the ID of the product:");

        while (true)
        {
            try
            {
                userSelectionId = int.Parse(Console.ReadLine());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine("Enter new stock threshold");

        while (true)
        {
            try
            {
                selectedStockThreshold = int.Parse(Console.ReadLine());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        foreach (var product in Inventory)
        {
            if (product.Id == userSelectionId)
            {
                product.ChangeStockThreshold(selectedStockThreshold);
            }
        }
    }
    #endregion

}
