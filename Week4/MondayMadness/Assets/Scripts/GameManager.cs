using UnityEngine;
using TMPro;            // This contains TextMeshProUGUI, which you should use for text


public class GameManager : MonoBehaviour
{
    // All game States
    private enum State { None, Asleep, YourOffice, ComputerScreen, MeetingRoom, AccountsOffice, BreakRoom, BossOffice, GoodEnd, BadEnd, SecretEnd }
    private enum MonitorState { Main, BIRA, EMail, CMD }

    [SerializeField] AudioManager audioManager;
    [SerializeField] GameObject imageCanvas;
    private ImageManager imageManager;
    // Panel, holding the main menu objects
    [SerializeField] GameObject startItems;

    // This is a parent object that contains your entire menu panel
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject walkthroughPanel;

    // This is the panel, which holds all buttons and text
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject userInput;

    // Main Text element
    [SerializeField] TextMeshProUGUI storyText;

    // Choice Text elements - Must be interactable (event handler)
    [SerializeField] TextMeshProUGUI choiceAText;
    [SerializeField] TextMeshProUGUI choiceBText;
    [SerializeField] TextMeshProUGUI choiceCText;
    [SerializeField] TextMeshProUGUI choiceDText;

    // These variables are for holding the current state, as well as the previous state
    private State currentState;
    private State previousState;
    private MonitorState monitorState;

    // Define the conditions
    private static bool hasBiraTasks = false;
    private static bool hasEmails = false;
    private static bool hasMeetingIntel = hasBiraTasks && hasEmails;
    private static bool hasBottleOfPills = false;
    private static bool hasQuarterlyReport = false;
    private static bool afterMeeting = false;

    void Start()
    {
        imageManager = imageCanvas.GetComponent<ImageManager>();
        currentState = State.None;   // Our entry state will be 'None' - Main menu
        previousState = State.None; // There's no previous state yet
        monitorState = MonitorState.Main;
        DisplayState();
    }

    // This toggles the menu on or off
    public void ShowMenu() => menu.SetActive(!menu.activeSelf);
    public void ShowWalkthrough() => walkthroughPanel.SetActive(!walkthroughPanel.activeSelf);

    // This function is just for changing the texts, images and music. The logic for our state machine is in SelectChoice()
    // You can show different texts in the same state using the condition and previousState variables
    // It's maybe better to have a dictionary with all texts and do the changes in he choices funcion
    // Note 2: It's definitely 1000000% better to have some sort of a hierarchical structure, where you cann pull the texts by the current state
    // especially with my nested options
    void DisplayState()
    {
        switch (currentState)
        {
            case State.None:
                imageManager.ChangeImage("Menu");
                choiceAText.text = "";
                choiceBText.text = "";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.Asleep:
                imageManager.ChangeImage("Asleep");
                storyText.text = previousState != State.None ? $"You are asleep again" : "You are asleep";
                choiceAText.text = "Wake up";
                choiceBText.text = "Don't wake up";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.YourOffice:
                imageManager.ChangeImage("YourOffice");
                storyText.text = !hasMeetingIntel ? "It's Monday. You find yourself in your office and your daily meeting starts soon. Better get ready for it by checking your ongoing tasks!" : "It's Monday. You find yourself in your office. Your daily meeting starts soon - you can head there directly, since you already know your ongoing tasks.";
                choiceAText.text = "Check your computer";
                choiceCText.text = "Go to Accounting";
                choiceBText.text = hasMeetingIntel ? "Go to the meeting room" : "";
                choiceDText.text = !hasBottleOfPills ? "" : "Take a sleeping pill";
                break;

            case State.ComputerScreen:
                imageManager.ChangeImage("Monitor");
                switch (monitorState)
                {
                    case MonitorState.Main:
                        storyText.text = "You look at your monitor.";
                        choiceAText.text = "Open BIRA";
                        choiceBText.text = "Read Emails";
                        choiceCText.text = "Open shell";
                        choiceDText.text = "Step away from the computer";
                        break;
                    case MonitorState.BIRA:
                        storyText.text = "You look at your BIRA tasks.";
                        choiceAText.text = "Go back to desktop";
                        choiceBText.text = "";
                        choiceCText.text = "";
                        choiceDText.text = "";
                        break;
                    case MonitorState.EMail:
                        storyText.text = "You check your recent E-Mails";
                        choiceAText.text = "Go back to desktop";
                        choiceBText.text = "";
                        choiceCText.text = "";
                        choiceDText.text = "";
                        break;
                    case MonitorState.CMD:
                        storyText.text = "You open a shell";
                        choiceAText.text = "Go back to desktop";
                        choiceBText.text = "";
                        choiceCText.text = "";
                        choiceDText.text = "";
                        break;
                }
                break;

            case State.MeetingRoom:
                imageManager.ChangeImage("MeetingRoom");
                storyText.text = !afterMeeting ? "9:20AM, Monday: Nothing interesting in the meeting ... again. You actually feel sleepier than you did before you drank your morning coffee. Better go refuel!" : "Nobody here, I guess everybody went on with their day-to-day tasks...";
                choiceAText.text = "Go to break room";
                choiceBText.text = "Go back to your office";
                choiceCText.text = !hasBottleOfPills ? "" : "Take a sleeping pill";
                choiceDText.text = !afterMeeting ? "" : "Go to your boss's office";
                break;

            case State.AccountsOffice:
                imageManager.ChangeImage(afterMeeting ? "AccountsClosed" : "AccountsOpen");
                storyText.text = afterMeeting ? "You find a closed door with a note on it. It says: 'Account sheets audit @ 9:15AM Monday, will be gone for the rest of the day.'" : "You find yourself in the Accounts Office, there's nobody inside, but you see an open folder on the desk in front of you. It's the quarterly finance report and your name is on it. It seems your team did earn your boss a fortune these past few months!";
                choiceAText.text = afterMeeting ? "Go to your boss's office" : hasMeetingIntel ? "Go to the meeting room" : "Go to your office";
                choiceBText.text = !hasBottleOfPills ? "" : "Take a sleeping pill";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.BreakRoom:
                imageManager.ChangeImage("BreakRoom");
                storyText.text = "This is the break room. Let's take a quick break and go talk to the boss.";
                choiceAText.text = "Go to your boss's office";
                choiceBText.text = "Go to Accounting";
                choiceCText.text = !hasBottleOfPills ? "Examine the medical cabinet" : "Take a sleeping pill";
                choiceDText.text = "";
                break;

            case State.BossOffice:
                imageManager.ChangeImage("BossOffice");
                storyText.text = "You enter you boss's office. He tells you that he's really satisfied with your performance this quarter, but sadly the company can't afford to give you a raise just yet.";
                choiceAText.text = "You start feeling a little weak on your feet... You're about to faint!";
                choiceBText.text = hasQuarterlyReport ? "Tell the boss you read the quarterly report!" : "";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.GoodEnd:
                imageManager.ChangeImage("BossOffice");
                storyText.text = "Your boss tells you that you actually did earn him a lot of money this quarter - he just didn't want to give you a promotion so easiliy. You get promoted to Grand Master Operations Officer and can afford to also give hefty bonuses to your whole team. Everybody is happy. What a mad monday!";
                choiceAText.text = "Back to title";
                choiceBText.text = "";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.BadEnd:
                imageManager.ChangeImage("Dead");
                audioManager.PlayDeathMusic();
                storyText.text = "You Died";
                choiceAText.text = "Back to title";
                choiceBText.text = "";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            case State.SecretEnd:
                imageManager.ChangeImage("Happy");
                storyText.text = "After completely trashing your computer, you give it off to the IT department, explaining that 'it just stopped working somehow' and go home to take a well deserved long weekend!";
                choiceAText.text = "Back to title";
                choiceBText.text = "";
                choiceCText.text = "";
                choiceDText.text = "";
                break;

            default:
                break;
        }

        // These statements deactivate all choice texts that aren't containing any texts
        // If you just set the text to be empty, you might still be able to click on the UI element and trigger the event
        choiceAText.transform.parent.gameObject.SetActive(choiceAText.text != "");
        choiceBText.transform.parent.gameObject.SetActive(choiceBText.text != "");
        choiceCText.transform.parent.gameObject.SetActive(choiceCText.text != "");
        choiceDText.transform.parent.gameObject.SetActive(choiceDText.text != "");

        // This is if we're in the main menu
        if (choiceAText.text == "" && choiceBText.text == "" && choiceCText.text == "" && choiceDText.text == "")
        {
            imageCanvas.SetActive(false);
            audioManager.SetGameMusic(true);
            userInput.SetActive(false);
            startItems.SetActive(true);
            controlsPanel.SetActive(false);
            menuButton.SetActive(false);
            menu.SetActive(false);
            walkthroughPanel.SetActive(false);
        }
        else if (currentState == State.ComputerScreen && monitorState == MonitorState.CMD)
        {
            userInput.SetActive(true);
        }
        else
        {
            imageCanvas.SetActive(true);
            userInput.SetActive(false);
            startItems.SetActive(false);
            menuButton.SetActive(true);
            controlsPanel.SetActive(true);
        }
    }

    // This function contains the actual logic for our state machine
    // The choice parameter is given by the OnClick() or OnPointerDown() event found on the Button / Event Trigger component
    public void SelectChoice(int choice)
    {
        audioManager.PlayClick();
        previousState = currentState;

        switch (currentState)
        {
            case State.None:
                hasBiraTasks = false;
                hasEmails = false;
                hasMeetingIntel = hasBiraTasks && hasEmails;
                hasBottleOfPills = false;
                hasQuarterlyReport = false;
                afterMeeting = false;
                currentState = State.Asleep;
                monitorState = MonitorState.Main;
                audioManager.SetGameMusic(false);
                break;

            case State.Asleep:
                if (choice == 1)
                {
                    afterMeeting = false;
                    hasBottleOfPills = false;
                    currentState = State.YourOffice;
                }
                else if (choice == 2) currentState = State.BadEnd;
                break;

            case State.YourOffice:
                if (choice == 1) currentState = State.ComputerScreen;
                else if (choice == 2) currentState = State.MeetingRoom;
                else if (choice == 3) currentState = State.AccountsOffice;
                else if (choice == 4) currentState = State.Asleep;
                break;

            case State.ComputerScreen:
                switch (monitorState)
                {
                    case MonitorState.Main:
                        if (choice == 1)
                        {
                            currentState = State.ComputerScreen;
                            monitorState = MonitorState.BIRA;
                            hasBiraTasks = true;
                            hasMeetingIntel = hasBiraTasks && hasEmails;
                        }
                        else if (choice == 2)
                        {
                            currentState = State.ComputerScreen;
                            monitorState = MonitorState.EMail;
                            hasEmails = true;
                            hasMeetingIntel = hasBiraTasks && hasEmails;
                        }
                        else if (choice == 3)
                        {
                            currentState = State.ComputerScreen;
                            monitorState = MonitorState.CMD;
                            hasMeetingIntel = hasBiraTasks && hasEmails;
                        }
                        else if (choice == 4) currentState = State.YourOffice;
                        break;
                    case MonitorState.BIRA:
                        if (choice == 1) currentState = State.ComputerScreen; monitorState = MonitorState.Main;
                        break;
                    case MonitorState.EMail:
                        if (choice == 1) currentState = State.ComputerScreen; monitorState = MonitorState.Main;
                        break;
                    case MonitorState.CMD:
                        if (choice == 1) { currentState = State.ComputerScreen; monitorState = MonitorState.Main; }
                        else if (choice == 42) currentState = State.SecretEnd;
                        break;
                    default:
                        break;
                }
                break;

            case State.MeetingRoom:
                afterMeeting = true;
                if (choice == 1) currentState = State.BreakRoom;
                else if (choice == 2) currentState = State.YourOffice;
                else if (choice == 3) currentState = State.Asleep;
                else if (choice == 4) currentState = State.BossOffice;
                break;

            case State.AccountsOffice:
                if (!afterMeeting) { hasQuarterlyReport = true; }
                if (choice == 1)
                {
                    currentState = afterMeeting ? State.BossOffice : hasMeetingIntel ? State.MeetingRoom : State.YourOffice;
                }
                else if (choice == 2) currentState = State.Asleep;
                break;

            case State.BreakRoom:
                if (choice == 1) currentState = State.BossOffice;
                else if (choice == 2) currentState = State.AccountsOffice;
                else if (choice == 3)
                {
                    currentState = !hasBottleOfPills ? State.BreakRoom : State.Asleep;
                    hasBottleOfPills = true;
                }
                break;

            case State.BossOffice:
                if (choice == 1) currentState = State.Asleep;
                else if (choice == 2) currentState = State.GoodEnd;
                break;

            case State.GoodEnd:
                if (choice == 1) currentState = State.None;
                break;

            case State.BadEnd:
                if (choice == 1) currentState = State.None;
                break;

            case State.SecretEnd:
                if (choice == 1) currentState = State.None;
                break;

            default:
                break;
        }
        if (choice == 0) currentState = State.None;
        DisplayState();
    }
}