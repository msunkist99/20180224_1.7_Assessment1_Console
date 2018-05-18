using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180224_1._7_Assessment1_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "_201800307_1.7_Assessment1_Console";

            Console.WriteLine("Welcome to - The I Don't Care - Cafe");

            string selection = "";
            bool valid = false;

            int seatingChoiceInt = 0;
            int mealChoiceInt = 0;
            string[] mealChoiceDesc = new string[] { "", "breakfast", "lunch", "dinner" };
            string[] seatingChoiceDesc = new string[] { "", "a table", "a booth", "a to go order" };

            Console.WriteLine("\r\nWould you like a - ");

            while (valid == false)
            {
                Console.WriteLine("1 - table");
                Console.WriteLine("2 - booth");
                Console.WriteLine("3 - this is a ToGo order");

                selection = Console.ReadLine();

                if ((selection == "1") || (selection == "2") || (selection == "3"))
                {
                    valid = true;
                    seatingChoiceInt = int.Parse(selection);
                }
                else
                {
                    Console.WriteLine("\r\n Your seating selection is not valid - please enter 1, 2, or 3");
                }
            }

            valid = false;

            Console.WriteLine("\r\nWill you be joining us for - ");

            while (valid == false)
            {
                Console.WriteLine("1 - breakfast");
                Console.WriteLine("2 - lunch");
                Console.WriteLine("3 - dinner");

                selection = Console.ReadLine();

                if ((selection == "1") || (selection == "2") || (selection == "3"))
                {
                    valid = true;
                    mealChoiceInt = int.Parse(selection);
                }
                else
                {
                    Console.WriteLine("\r\n Your meal selection is not valid - please enter 1, 2, or 3");
                }
            }

            MenuItem[] menuItemsArray = LoadMenuItems(mealChoiceInt);
            int i = 1;
            int menuChoiceInt = 0;

            valid = false;

            while (valid == false)
            {
                Console.WriteLine("\r\n Your menu selections - ");
                foreach (MenuItem item in menuItemsArray)
                {
                    Console.WriteLine(item.itemRestriction + i + " - " + item.itemName + "\t" + item.itemCost);
                    ++i;
                }

                selection = Console.ReadLine();

                try
                {
                    menuChoiceInt = int.Parse(selection);
                    if ((menuChoiceInt > 0) && (menuChoiceInt < menuItemsArray.Length + 1))
                    {
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("\r\n Your menu selection is not valid -");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\r\n Your menu selection is not valid -");
                }
            }

            MenuItem[] drinkItemsArray = LoadDrinkItems(mealChoiceInt);

            bool alcoholicBeverage = false;
            DateTime legalAgeDate = GetLegalAgeDate(21);
            int drinkChoiceInt = 0;

            valid = false;

            while (valid == false)
            {
                DateTime inputBirthDate = DateTime.MaxValue;
                i = 1;

                Console.WriteLine("\r\n Your drink selections - ");

                foreach (MenuItem item in drinkItemsArray)
                {
                    Console.WriteLine(item.itemRestriction + i + " - " + item.itemName + "\t" + item.itemCost);
                    ++i;

                    if (item.itemRestriction == "*")
                    {
                        alcoholicBeverage = true;
                    }
                }

                if (alcoholicBeverage == true)
                {
                    Console.WriteLine(" * - Your birthdate must be on or before - {0} - to order this beverage", legalAgeDate.ToShortDateString());
                }

                selection = Console.ReadLine();

                try
                {
                    drinkChoiceInt = int.Parse(selection);
                    if ((drinkChoiceInt > 0) && (drinkChoiceInt < drinkItemsArray.Length + 1))
                    {
                        if (drinkItemsArray[drinkChoiceInt - 1].itemRestriction == "*")
                        {
                            while (inputBirthDate == DateTime.MaxValue)
                            {
                                inputBirthDate = ValidateInputBirthdate();
                            }
                            valid = CheckAge(legalAgeDate, inputBirthDate);
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\r\n Your drink selection is not valid -");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\r\n Your drink selection is not valid -");
                }
            }

            Console.WriteLine("\r\n\r\n\tYou requested {0} for {1}", seatingChoiceDesc[seatingChoiceInt], mealChoiceDesc[mealChoiceInt]);
            Console.WriteLine("\tYour menu selection - {0} {1:C}", menuItemsArray[menuChoiceInt - 1].itemName, menuItemsArray[menuChoiceInt - 1].itemCost);
            Console.WriteLine("\tYour drink selection - {0} {1:C}", drinkItemsArray[drinkChoiceInt - 1].itemName, drinkItemsArray[drinkChoiceInt - 1].itemCost);

            double subTotal = menuItemsArray[menuChoiceInt - 1].itemCost + drinkItemsArray[drinkChoiceInt - 1].itemCost;
            double suggestedTip = subTotal * .2;
            double total = subTotal + suggestedTip;

            Console.WriteLine("\tSubtotal - {0:C}", subTotal);
            Console.WriteLine("\tSuggested Tip - {0:C}", suggestedTip);
            Console.WriteLine("\tTotal - {0:C}", total);

            Console.ReadLine();
        }


        private static MenuItem[] LoadMenuItems(int mealChoiceInt)
        {
            if (mealChoiceInt == 1)
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="breakfast 1",itemCost=5.99, itemRestriction=" "},
                    new MenuItem(){itemName="breakfast 2",itemCost=6.99, itemRestriction=" "},
                    new MenuItem(){itemName="breakfast 3",itemCost=7.99, itemRestriction=" "}
                };
                return menuItems;
            }
            else if (mealChoiceInt == 2)
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="lunch 1",itemCost=8.99, itemRestriction=" "},
                    new MenuItem(){itemName="lunch 2",itemCost=9.99, itemRestriction=" "},
                    new MenuItem(){itemName="lunch 3",itemCost=10.99, itemRestriction=" "}
                };
                return menuItems;
            }
            else  //mealChoiceInt == 3
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="dinner 1",itemCost=11.99, itemRestriction=" "},
                    new MenuItem(){itemName="dinner 2",itemCost=12.99, itemRestriction=" "},
                    new MenuItem(){itemName="dinner 3",itemCost=13.99, itemRestriction=" "}
                };
                return menuItems;
            }
        }

        private static MenuItem[] LoadDrinkItems(int mealChoiceInt)
        {
            if (mealChoiceInt == 1)
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="breakfast drink 1",itemCost=5.89, itemRestriction=" "}
                };
                return menuItems;
            }
            else if (mealChoiceInt == 2)
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="lunch drink 1",itemCost=6.89, itemRestriction=" "},
                    new MenuItem(){itemName="lunch drink 2",itemCost=7.89, itemRestriction="*"},
                    new MenuItem(){itemName="lunch drink 3",itemCost=8.89, itemRestriction=" " }

                };
                return menuItems;
            }
            else  //menuSelectionInt == 3
            {
                MenuItem[] menuItems = new MenuItem[]
                {
                    new MenuItem(){itemName="dinner drink 1", itemCost=11.89, itemRestriction=" "},
                    new MenuItem(){itemName="dinner drink 2", itemCost=12.89, itemRestriction=" "},
                    new MenuItem(){itemName="dinner drink 3", itemCost=13.89, itemRestriction="*"}
                };
                return menuItems;
            }
        }

        private class MenuItem
        {
            public string itemName { get; set; }
            public double itemCost { get; set; }
            public string itemRestriction { get; set; }
        }

        private static DateTime GetLegalAgeDate(int intYears)
        {
            return DateTime.Now.AddYears(-1 * intYears);
        }

        private static DateTime ValidateInputBirthdate()
        {
            Console.Write("Enter your birthdate as mm/dd/yyyy: ");
            DateTime birthdate = DateTime.MaxValue;

            try
            {
                birthdate = DateTime.Parse(Console.ReadLine());

            }
            catch (Exception)
            {
                Console.WriteLine("you entered an invalid birthdate");
            }

            return birthdate;
        }

        private static bool CheckAge(DateTime legalAge, DateTime inputBirthDate)
        {
            if (inputBirthDate.CompareTo(legalAge) == 1)
            {
                Console.WriteLine("Sorry but you are not 21 - please select make a different drink selection");
                return false;
            }
            else
            {
                Console.WriteLine("Thank you for verifying your age.");
                return true;
            }
        }
    }
}

