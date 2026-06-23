using System.Reflection.Metadata;

string actualUsername = "Balázs";
string actualPassword = "Alma";
bool appRunning = true;
DateTime lastLoginTime = DateTime.MinValue;

while (appRunning)
{
    Console.Clear();
    Console.WriteLine("Welcome back! Please log in below.");
    Console.Write("Username: ");
    string userProvidedName = Console.ReadLine();
    Console.Write("Password: ");
    string userProvidedPassword = Console.ReadLine();

    if (!LoginAttempts(userProvidedName, userProvidedPassword, actualUsername, actualPassword, out lastLoginTime))
    {
        appRunning = false;
        continue;
    }

    bool stayInMenu = true;
    while (stayInMenu)
    {
        (bool keepShowingMenu, bool exitApplication) = adminMenu(ref actualUsername, ref actualPassword, lastLoginTime);
        stayInMenu = keepShowingMenu;
        if (exitApplication)
        {
            appRunning = false;
        }
    }
}

static bool LoginAttempts(string userProvidedName, string userProvidedPassword, string actualUsername, string actualPassword, out DateTime lastLoginTime)
{
    int attempts = 0;
    while (attempts < 3)
    {
        if (userProvidedName == actualUsername && userProvidedPassword == actualPassword)
        {
            lastLoginTime = DateTime.Now;
            Console.WriteLine("Login successful! Welcome back, " + actualUsername + "!");
            return true;
        }

        else
        {
            Console.WriteLine("Login wasn't successful! Please check your username and password and try again!");
            attempts++;

            if (attempts < 3)
            {
                Console.Write("Username: ");
                userProvidedName = Console.ReadLine();
                Console.Write("Password: ");
                userProvidedPassword = Console.ReadLine();
            }
            if (attempts == 3)
            {
                Console.WriteLine("Too many failed attempts! Please try again later.");
            }
            
        }
    }
    lastLoginTime = DateTime.MinValue;
    return false;
}

static (bool keepShowingMenu, bool exitApplication) adminMenu(ref string actualUserName, ref string actualPassword, DateTime lastLoginTime)
{
    Console.Clear();
    Console.WriteLine("Choose an option");
    Console.WriteLine("1) Change password");
    Console.WriteLine("2) Change username");
    Console.WriteLine("3) Account info");
    Console.WriteLine("4) Message encoder");
    Console.WriteLine("5) Timezone converter");
    Console.WriteLine("6) Number guessing game");
    Console.WriteLine("7) Reaction time game");
    Console.WriteLine("8) Reverser");
    Console.WriteLine("9) Log out");
    Console.WriteLine("10) Leave");

    string? userChoice = Console.ReadLine();

    if (userChoice == "1")
    {
        changePassword(ref actualPassword);
        return (true, false);
    }
    else if (userChoice == "2")
    {
        changeUserName(ref actualUserName);
        return (true, false);
    }
    else if (userChoice == "3")
    {
        accountInfo(actualUserName, actualPassword, lastLoginTime);
        return (true, false);
    }

    else if (userChoice == "4")
    {
        MessageEncoder();
        return (true, false);
    }

    else if (userChoice == "5")
    {
        TimezoneConverter();
        return (true, false);
    }

    else if (userChoice == "6")
    {
        NumberGuessingGame();
        return (true, false);
    }
    else if (userChoice == "7")
    {
        ReactionTimeGame();
        return (true, false);
    }
    else if (userChoice == "8")
    {
        Reverser();
        return (true, false);
    }
    else if (userChoice == "9")
    {
        LogOut();
        return (false, false);
    }
    else if (userChoice == "10")
    {
        return (false, true);
    }

    Console.WriteLine("Invalid option.");
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
    return (true, false);
}

static void NumberGuessingGame()
{
    Random numberGenerator = new Random();
    int numberToGuess = numberGenerator.Next(1, 101);
    int userGuess = 0;
    int attempts = 0;
    int minPossible = 1;
    int maxPossible = 100;

    Console.WriteLine("I've picked a number between 1 and 100.");
    while (userGuess != numberToGuess)
    {
        Console.Write($"Enter your guess ({minPossible}-{maxPossible}): ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out userGuess) || userGuess < 1 || userGuess > 100)
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 100.");
            continue;
        }

        attempts++;

        if (userGuess < numberToGuess)
        {
            Console.WriteLine("Too low! Try again.");
            minPossible = userGuess + 1;
        }
        else if (userGuess > numberToGuess)
        {
            Console.WriteLine("Too high! Try again.");
            maxPossible = userGuess - 1;
        }
        else
            Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts.");
    }

    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
}

static void Reverser()
{
    Console.WriteLine("Enter what to reverse:");
    string reverseInput = Console.ReadLine();
    char[] charArray = reverseInput.ToCharArray();
    Array.Reverse(charArray);
    string reversedString = new string(charArray);
    Console.WriteLine("Reversed string: " + reversedString);
    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
}

static void changePassword(ref string actualPassword)
{
    Console.Write("Enter your current password: ");
    string currentPassword = Console.ReadLine();
    if (currentPassword == actualPassword)
    {
        Console.Write("Enter your new password: ");
        string newPassword = Console.ReadLine();
        actualPassword = newPassword;
        Console.WriteLine("Password changed successfully!");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }

    else
    {
        Console.WriteLine("Incorrect Password, please try again!");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }
}

static void changeUserName(ref string actualUserName)
{
    Console.Write("Enter your current username: ");
    string currentUserNameInput = Console.ReadLine();
    if (currentUserNameInput == actualUserName)
    {
        Console.Write("Enter your new username: ");
        string newUserName = Console.ReadLine();
        actualUserName = newUserName;
        Console.WriteLine("Username changed successfully!");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }

    else
    {
        Console.WriteLine("Incorrect username, please try again!");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }
}

static void accountInfo(string actualUserName, string actual, DateTime lastLoginTime)
{
    Console.WriteLine($"Username: {actualUserName}");
    Console.WriteLine($"Password: {actual}");
    Console.WriteLine("If you wish to change these, you can do so by selecting their options in the menu!");
    Console.WriteLine("Last time logged in: " + lastLoginTime);
    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
}

static void LogOut()
{
    Console.WriteLine("You have been logged out.");
    Console.WriteLine("Press Enter to return to the login screen...");
    Console.ReadLine();
}

static void MessageEncoder()
{
    Console.Clear();
    Console.WriteLine("Do you want to encode or decode a message?");
    string choice = Console.ReadLine().ToLower();

    if (choice == "encode")
    {
        Console.WriteLine("Enter a message to Encode: ");
        string message = Console.ReadLine();
        string encodedMessage = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message));
        Console.WriteLine($"Encoded message: {encodedMessage}");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
        
    }

    else if (choice == "decode")
    {
        Console.WriteLine("Enter a message to Decode: ");
        string encodedMessage = Console.ReadLine();
        try
        {
            string decodedMessage = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedMessage));
            Console.WriteLine($"Decoded message: {decodedMessage}");
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Base64 string. Please ensure the input is correctly formatted.");
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }

    else
    {
        Console.WriteLine("Invalid choice. Please enter 'encode' or 'decode'.");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }
}

// Timezone converter method is currently not working, it will be commented out until it is not fixed. 

static void TimezoneConverter()
{
    Console.WriteLine("Timezone converter is currently unavailable. Please check back later.");
    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
    /*
    Console.Clear();
    Console.WriteLine("Enter the time you want to convert (in HH:mm format): ");
    string timeInput = Console.ReadLine();

    Console.WriteLine("Enter the source timezone (e.g., 'Eastern Standard Time'): ");
    string sourceTimezone = Console.ReadLine();

    Console.WriteLine("Enter the target timezone (e.g., 'Pacific Standard Time'): ");
    string targetTimezone = Console.ReadLine();

    try
    {
        DateTime time = DateTime.ParseExact(timeInput, "HH:mm", null);
        TimeZoneInfo sourceTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(sourceTimezone);
        TimeZoneInfo targetTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(targetTimezone);

        DateTime sourceTime = TimeZoneInfo.ConvertTime(time, sourceTimeZoneInfo);
        DateTime targetTime = TimeZoneInfo.ConvertTime(sourceTime, targetTimeZoneInfo);

        Console.WriteLine($"The time {timeInput} in {sourceTimezone} is {targetTime:HH:mm} in {targetTimezone}.");
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid time format. Please enter the time in HH:mm format.");
    }
    catch (TimeZoneNotFoundException)
    {
        Console.WriteLine("One or both of the specified timezones were not found. Please check your input and try again.");
    }
    catch (InvalidTimeZoneException)
    {
        Console.WriteLine("One or both of the specified timezones are invalid. Please check your input and try again.");
    }

    
    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
    */
}

static void ReactionTimeGame()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Reaction time game!");
    Console.WriteLine("Press Enter to start...");
    Console.ReadLine();
    Random rand = new Random();
    int waitTime = rand.Next(2000, 5000);
    System.Threading.Thread.Sleep(waitTime);
    Console.WriteLine("Press Enter now!");
    DateTime startTime = DateTime.Now;
    Console.ReadLine();
    DateTime endTime = DateTime.Now;
    TimeSpan reactionTime = endTime - startTime;
    Console.WriteLine($"Your reaction time was: {reactionTime.TotalMilliseconds} milliseconds.");

    if (reactionTime.TotalMilliseconds > 0)
    {
        Console.WriteLine("Hey, you jumped the start! Please try again and wait for the prompt before pressing Enter.");
    }

    else if (reactionTime.TotalMilliseconds < 200)
    {
        Console.WriteLine("Wow! That is impressive! You have lightning-fast reflexes!");
    }

    else if (reactionTime.TotalMilliseconds < 300)
    {
        Console.WriteLine("Good job! You have above average reaction time!");
    }

    else if (reactionTime.TotalMilliseconds < 400)
    {
        Console.WriteLine("Not bad! You have average reaction time!");
    }

    else if (reactionTime.TotalMilliseconds == 394.949)
    {
        Console.WriteLine("We have the same reaction time! What a coincidence!");
    }

    else
    {
        Console.WriteLine("You might want to work on your reaction time. Keep practicing!");
    }

    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
}
