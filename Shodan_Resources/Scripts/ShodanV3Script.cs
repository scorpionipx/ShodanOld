using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Security.AccessControl;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Xml;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using WikiReader;

namespace Shodan3._0
{
    public partial class Shodan : Form
    {
        /*VARIABLES*/
        string ask_to_proceed;
        string baloon_tip_info;
        string check_password_for;
        string hooked_key;
        string hooked_log;
        string input_string_method;
        string password;
        string pass_retype;
        string shodan_state;
        string username;
        String xml_content;
        int button_mouse_hover;
        int input_password_stage;
        int timer1Tiker;
        Boolean buttons_visible;
        Boolean speaking_allowed;
        Boolean firsttime;
        Boolean forcedclosed;
        Boolean keylogger_recording;
        Boolean listening;
        Boolean password_required;
        Boolean ready_for_new_command;
        Boolean remote_controlled;
        Boolean show_ready_message;
        bool text_box_first_time_opened;
        bool text_box1_fisrt_time_opened;
        Random rnd = new Random();/*rnd.Next(1, 13) returns a number between 1 and 12*/
        UserActivityHook hook;/*USED AS ENGINE FOR KEYLOGGER PROTOCOL*/
        string[] byereply = new string[] { "See you later.", "Good bye.", "Take care.", "Until next time.", "Turning off. Bye." };
        string[] readyreply = new string[] { "What's the first task?", "I'm locked and loaded", "I'm ready to work.", "What you want me to do?", "Waiting for your command." };

        /*SPEECH SYNTHESIZER*/
        SpeechSynthesizer reply = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        /*SPEECH SYNTHESIZER*/

        /*SPEECH RECOGNITION*/
        SpeechRecognitionEngine sr = new SpeechRecognitionEngine();

        /*GRAMMARS*/
        Grammar AdvancedCommands_Grammar;
        Grammar AskCommands_Grammar;
        Grammar BasicCommands_Grammar;
        Grammar CommandsPannelCommands_Grammar;
        Grammar ComputerCommands_Grammar;
        Grammar KeyLoggerCommands_Grammar;
        Grammar Listening_Grammar;
        Grammar SecurityCommands_Grammar;
        Grammar SettingsCommands_Grammar;
        Grammar StringCommands_Grammar;
        Grammar TextboxCommands_Grammar;
        /*GRAMMARS*/

        /*COMMANDS STRINGS*/
        static string[] advancedcommands_string = new string[] {"Show networks", "Scan devices", "Key logger", "Remote control", "Basic commands"};
        static string[] askcommands_string = new string[] {"Yes", "No"};
        static string[] basiccommands_string = new string[] {"Advanced commands", "Security commands", "Show textbox", "Machine commands", "Open disk tray", "Say the time", "Change settings", "Minimize yourself", "Maximize yourself", "Go to system tray", "Show yourself", "Take a break" };
        static string[] commandspannelcommands_string = new string[] { "Show commands pannel", "Hide commands pannel" };
        static string[] computercommands_string = new string[] { "Shut down", "Restart", "System properties", "Basic commands" };
        static string[] keyloggercommands_string = new string[] {"Start recording", "Stop recording", "Show key log", "Clear key log", "Basic commands" };
        static string[] listeningcommands_string = new string[] { "Listen again"};
        static string[] securitycommands_string = new string[] {"Basic locker", "Basic commands"};
        static string[] settingscommands_string = new string[] { "Change username", "Change password", "Show the log", "Reply mode", "Basic commands" };
        static string[] stringcommands_string = new string[] {"Input string", "Abord protocol"};
        static string[] textboxcommands_string = new string[] {"Read it all", "Read selected", "Clear content", "Clear selected", "Crypt text", "Decrypt text", "Basic commands" };
        /*COMMANDS STRINGS*/

        /*BALOON TIPS STRINGS*/
        string show_CommandsPannel_info = "Vocal command: \"" + commandspannelcommands_string[0] + "\"\nAction: Displays the list of available commands at the moment.";
        string hide_CommandsPannel_info = "Vocal command: \"" + commandspannelcommands_string[1] + "\"\nAction: Hides the list of available commands, if not\nhidden already.";
        string advanced_Commands_info = "Vocal command: \"" + basiccommands_string[0] + "\"\nAction: Enables use of the advanced commands.";
        string show_Networks_info = "Vocal command: \"" + advancedcommands_string[0] + "\"\nAction: If WI-FI is available and powered on, displays\nthe list of all available Wireless networks in vecinity\nand determines which is the most powerfull.";
        string read_Everything_info = "Vocal command: \"" + textboxcommands_string[0] + "\"\nAction: Reads the entire text wrote in the TextBox.\nSecondary: Enables use of the \"Stop Reading\" button.";
        string read_Selected_info = "Vocal command: \"" + textboxcommands_string[1] + "\"\nAction: Reads selected from TextBox.\nSecondary: Enables use of the \"Stop Reading\" button.";
        string clear_Content_info = "Vocal command: \"" + textboxcommands_string[2] + "\"\nAction: Deletes the entire text wrote\nin the TextBox.";
        string clear_Selected_info = "Vocal command: \"" + textboxcommands_string[3] + "\"\nAction: Deletes selected text within the TextBox.";
        string change_Username_info = "Vocal command: \"" + settingscommands_string[0] + "\"\nAction: Initiates the changing username protocol.";
        string system_Info_info = "Vocal command: \"" + computercommands_string[2] + "\"\nAction: Displays system and machine information using\nDirectx Diagnostic Tool.";
        string ipx_CryptText_info = "Vocal command: \"" + textboxcommands_string[4] + "\"\nAction: Encrypts the text written in TextBox.\nScript: ScorpinoIPX Crypt";
        string ipx_DecryptText_info = "Vocal command: \"" + textboxcommands_string[5] + "\"\nAction: Decrypts a text written in TextBox crypted\nby ScorpionIPX Crypt Script.\nScript: ScorpinoIPX Decrypt";
        string scan_BluetoothDevices_info = "Vocal command: \"" + advancedcommands_string[1] + "\"\nAction: Scans for bluetooth devices in the area.\nIt requires a powered on bluetooth module.";
        string mobile_Shodan_info = "Vocal command: \"" + "NOT AVAILABLE YET" + "\"\nAction: Allows machine to be controlled by a remote\ndevice within mobile_Shodan Algorithm.";
        string start_Listening_info = "Vocal command: \"" + "NOT AVAILABLE YET" + "\"\nAction: Enables use of vocal commands.\nCurrent status: Vocal commands are turned off.";
        string stop_Listening_info = "Vocal command: \"" + "NOT AVAILABLE YET" + "\"\nAction: Disables use of vocal commands.\nCurrent status: Vocal commands are turned on.";
        /*BALOON TIPS STRINGS*/

        /*REMOTE BUTTONS TEXT STRINGS*/
        string[] advanced_remote_text = new string[] {"Networks", "Bluetooth", "Key logger", "Remote C.", "Disable R.C.", "Basic CMD"};
        string[] ask_remote_text = new string[] { "Yes", "No" };
        string[] basic_remote_text = new string[] {"Advanced", "Security", "Textbox", "Machine", "CD Tray", "Say time", "Settings", "Minimize", "Hide", "Exit"};
        string[] computer_remote_text = new string[] {"Turn OFF", "Restart", "System info", "Basic CMD"};
        string[] keylogger_remote_text = new string[] {"Start Rec.", "Stop Rec.", "Show K.L.", "Clear K.L.", "Basic CMD" };
        string[] security_remote_text = new string[] {"B. locker"};
        string[] settings_remote_text = new string[] {"Username", "Password", "Show log", "Switch reply", "Basic CMD"};
        string[] textbox_remote_text = new string[] {"Read all", "R. selected", "Clear all", "C. selected", "Crypt", "Decrypt", "Basic CMD"};
        /*REMOTE BUTTONS TEXT STRINGS*/
        /*SPEECH RECOGNITION*/

        /*CMD*/
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        /*CMD*/

        /*VARIABLES*/

        /*TEST*/
        private void TestBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void TestBtn2_Click(object sender, EventArgs e)
        {
            basic_Commands();
        }
        private void TestBtn3_Click(object sender, EventArgs e)
        {
            TestrichTextBox.Text = password;
        }
        private void Test3_Click(object sender, EventArgs e)
        {
            exit_Application();
        }
        /*TEST*/

        public Shodan()
        {
            InitializeComponent();
            initiate_Variables();
            all_RemoteButtonsInvisible();
            timer1.Start();
            initialize_Grammars();
            activate_BasicCommandsGrammar();
        }

        private void Shodan_Load(object sender, EventArgs e)
        {
            create_Directories();
            create_Files();
            read_Username();
            read_Password();
            log_Stamp();
            hello_User();
        }

        /*SPEECH RECOGNITION ENGINE'S FUNCTION*/
        void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text)
            {
                /*ALL*/
                case "Basic commands":
                    {
                        basic_Commands();
                        break;
                    }
                /*ALL*/

                /*ADVANCED*/
                case "Key logger":
                    {
                        check_Password("keylogger");
                        break;
                    }
                case "Remote control":
                    {
                        mobile_Shodan();
                        break;
                    }
                case "Scan devices":
                    {
                        scan_BluetoothDevices();
                        break;
                    }
                case "Show networks":
                    {
                        show_Networks();
                        break;
                    }
                /*ADVANCED*/

                /*ASK*/
                case "Yes":
                    {
                        answer_YES_Command();
                        break;
                    }
                case "No":
                    {
                        answer_NO_Command();
                        break;
                    }
                /*ASK*/

                /*BASIC*/
                case "Advanced commands":
                    {
                        advanced_Commands();
                        break;
                    }
                case "Machine commands":
                    {
                        computer_Commands();
                        break;
                    }
                case "Change settings":
                    {
                        settings_Commands();
                        break;
                    }
                case "Go to system tray":
                    {
                        hide_Shodan();
                        break;
                    }
                case "Maximize yourself":
                case "Show yourself":
                    {
                        TestrichTextBox.Text = "gunoi";
                        break;
                    }
                case "Minimize yourself":
                    {
                        minimize_Shodan();
                        break;
                    }
                case "Open disk tray":
                    {
                        open_CD_Tray();
                        break;
                    }
                case "Say the time":
                    {
                        say_Time();
                        break;
                    }
                case "Security commands":
                    {
                        security_Commands();
                        break;
                    }
                case "Show textbox":
                    {
                        show_Textbox();
                        break;
                    }
                case "Take a break":
                    {
                        exit_Application();
                        break;
                    }
                /*BASIC*/

                /*COMMANDS PANNEL*/
                case "Hide commands pannel":
                    {
                        hide_CommandsPannel();
                        break;
                    }
                case "Show commands pannel":
                    {
                        show_CommandsPannel();
                        break;
                    }
                /*COMMANDS PANNEL*/

                /*COMPUTER*/
                case "Restart":
                    {
                        ask_for_restart_Computer();
                        break;
                    }
                case "Shut down":
                    {
                        ask_for_turn_OFF_Computer();
                        break;
                    }
                case "System properties":
                    {
                        system_Info();
                        break;
                    }
                /*COMPUTER*/

                /*KEYLOGGER*/
                case "Show key log":
                    {
                        show_KeyLog();
                        break;
                    }
                /*KEYLOGGER*/

                /*STRING*/
                case "Input string":
                    {
                        input_String();
                        break;
                    }
                case "Abord protocol":
                    {
                        abord_InputString();
                        break;
                    }
                /*STRING*/

                /*SETTINGS*/
                case "Change password":
                    {
                        change_Password();
                        break;
                    }
                case "Change username":
                    {
                        change_Username();
                        break;
                    }
                case "Show the log":
                    {
                        check_Password("show_log");
                        break;
                    }
                case "Reply mode":
                    {
                        switch_Reply();
                        break;
                    }
                /*SETTINGS*/

                /*TEXTBOX*/
                case "Read it all":
                    {
                        read_Everything();
                        break;
                    }
                case "Read selected":
                    {
                        read_Selected();
                        break;
                    }
                case "Clear content":
                    {
                        clear_Content();
                        break;
                    }
                case "Clear selected":
                    {
                        clear_Selected();
                        break;
                    }
                case "Crypt text":
                    {
                        ipx_CryptText();
                        TestrichTextBox.Text = "Crypt";
                        break;
                    }
                case "Decrypt text":
                    {
                        ipx_DecryptText();
                        break;
                    }
                /*TEXTBOX*/
            }
        }
        /*SPEECH RECOGNITION ENGINE'S FUNCTION*/

        /*FUNCTIONS*/
        private void abord_InputString()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (input_string_method == "username")
                {
                    activate_SettingsCommandsGrammar();
                    buttons_SettingsRemoteText();
                    buttons_SettingsRemoteLoad();
                    history_Text("Canceling changing username protocol...");
                    speak("Changing username protocol has been canceled.");
                }
                else if (input_string_method == "password_change")
                {
                    activate_SettingsCommandsGrammar();
                    buttons_SettingsRemoteText();
                    buttons_SettingsRemoteLoad();
                    history_Text("Canceling changing password protocol...");
                    speak("Changing password protocol has been canceled.");
                    if(input_password_stage == 1 || input_password_stage == 2)
                    {
                        input_password_stage = 0;
                    }
                }
                else if (input_string_method == "password_assign")
                {
                    activate_SettingsCommandsGrammar();
                    buttons_SettingsRemoteText();
                    buttons_SettingsRemoteLoad();
                    history_Text("Canceling password assign protocol...");
                    speak("Assigning password protocol has been canceled.");
                    if (input_password_stage == 1)
                    {
                        input_password_stage = 0;
                    }
                }
                else if (input_string_method == "check_password")
                {
                    switch(check_password_for)
                    {
                        case "say_time":
                            {
                                activate_SettingsCommandsGrammar();
                                buttons_SettingsRemoteText();
                                buttons_SettingsRemoteLoad();
                                break;
                            }
                    }
                    history_Text("Canceling password check protocol...");
                    speak("Password check protocol has been canceled.");
                }
                InputButton.Visible = false;
                AbordButton.Visible = false;
                textBox1.Visible = false;
                text_box1_fisrt_time_opened = true;
                textBox1.Text = string.Empty;
            }
        }

        private void AbordButton_Click(object sender, EventArgs e)
        {
            abord_InputString();
        }

        public void activate_AdvancedCommandsGrammar()
        {
            shodan_state = "Advanced";
            generate_AdvancedCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_AskCommandsGrammar()
        {
            shodan_state = "Ask";
            generate_AskCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_BasicCommandsGrammar()
        {
            shodan_state = "Basic";
            generate_BasicCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_ComputerCommandsGrammar()
        {
            shodan_state = "Computer";
            generate_ComputerCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_KeyloggerCommandsGrammar()
        {
            shodan_state = "Keylogger";
            generate_KeyloggerCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_Listening_Grammar()
        {
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = false;/*ALWAYS TRUE*//*ONLY EXCEPTION*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_SecurityCommandsGrammar()
        {
            shodan_state = "Security";
            generate_SecurityCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_SettingsCommandsGrammar()
        {
            shodan_state = "Settings";
            generate_SettingsCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_StringCommandsGrammar()
        {
            shodan_state = "String";
            generate_StringCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            sr.RequestRecognizerUpdate();
        }

        public void activate_TextboxCommandsGrammar()
        {
            shodan_state = "Textbox";
            generate_TextboxCommandsList();
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;/*ALWAYS TRUE*/
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = true;
            sr.RequestRecognizerUpdate();
        }

        public void all_RemoteButtonsInvisible()
        {
            ListenBtn.Visible = false;
            ShowCPbtn.Visible = false;
            HideCPbtn.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
        }

        public void advanced_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_AdvancedRemoteText();
                buttons_AdvancedRemoteLoad();
                history_Text("Accessing advanced commands...");
                activate_AdvancedCommandsGrammar();
                speak("Advanced commands enabled.");
            }
        }

        public void answer_NO_Command()
        {
            switch (ask_to_proceed)
            {
                case "shutdown":
                    {
                        if(ready_for_new_command == true)
                        {
                            ready_for_new_command = false;
                            buttons_ComputerRemoteText();
                            buttons_ComputerRemoteLoad();
                            activate_ComputerCommandsGrammar();
                            history_Text("Shutdown procedure aborded.");
                            speak("Shutdown procedure aborded.");
                        }
                        break;
                    }
                case "restart":
                    {
                        if (ready_for_new_command == true)
                        {
                            ready_for_new_command = false;
                            buttons_ComputerRemoteText();
                            buttons_ComputerRemoteLoad();
                            activate_ComputerCommandsGrammar();
                            history_Text("Restart procedure aborded.");
                            speak("Restart procedure aborded.");
                        }
                        break;
                    }
                case "keylogger_clearlog":
                    {
                        if (ready_for_new_command == true)
                        {
                            ready_for_new_command = false;
                            buttons_KeyloggerRemoteText();
                            buttons_KeyloggerRemoteLoad();
                            activate_KeyloggerCommandsGrammar();
                            history_Text("Clear key log register procedure aborded.");
                            speak("Clear key log register procedure aborded.");
                        }
                        break;
                    }
            }
        }

        public void answer_YES_Command()
        {
                switch (ask_to_proceed)
                {
                    case "shutdown":
                        {
                            turn_OFF_Computer();
                            break;
                        }
                    case "restart":
                        {
                            restart_Computer();
                            break;
                        }
                case "keylogger_clearlog":
                    {
                        keylogger_ClearLog();
                        break;
                    }
                }
        }

        public void ask_Command()
        {
            buttons_AskRemoteText();
            buttons_AskRemoteLoad();
            activate_AskCommandsGrammar();
            history_Text("Are you sure you want to proceed?");
            switch (ask_to_proceed)
                {
                    case "shutdown":
                        {
                            speak("Are you sure you want to turn off your computer?");
                            break;
                        }
                    case "restart":
                        {
                            speak("Are you sure you want to restart your computer?");
                            break;
                        }
                case "keylogger_clearlog":
                    {
                        speak("Are you sure you want to clear key log register?");
                        break;
                    }
            }
        }

        public void ask_for_keylogger_clearLog()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (new FileInfo("Shodan\\Log\\keylog.ipx").Length == 0)
                {
                    history_Text("There is no record in the key log register.");
                    speak("There is no record in the key log register.");
                }
                else
                {
                    shodan_state = "Ask";
                    ask_to_proceed = "keylogger_clearlog";
                    ask_Command();
                }
            }
        }

        public void ask_for_restart_Computer()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                shodan_state = "Ask";
                ask_to_proceed = "restart";
                ask_Command();
            }
        }

        public void ask_for_turn_OFF_Computer()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                shodan_state = "Ask";
                ask_to_proceed = "shutdown";
                ask_Command();
            }
        }

        public void basic_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_BasicRemoteText();
                buttons_BasicRemoteLoad();
                history_Text("Accessing basic commands...");
                if (TextBox.Visible == true)
                {
                    TextBox.Visible = false;
                }
                activate_BasicCommandsGrammar();
                speak("Back to basic commands.");
            }
        }

        private async void scan_BluetoothDevices()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                show_ready_message = false;
                history_Text("Scan for bluetooth devices protocol engaged.");
                sr.RecognizeAsyncStop();
                pb.AppendText("Scan for bluetooth devices protocol engaged.");
                reply.SpeakAsync(pb);
                pb.ClearContent();
                try
                {
                    BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
                    history_Text("Scanning for bluetooth devices...");
                    sr.RecognizeAsyncStop();
                    pb.AppendText("Scanning.");
                    reply.Speak(pb);
                    pb.ClearContent();
                    richTextBox1.Text = string.Empty;
                    await Task.Delay(50);
                    BluetoothClient client = new BluetoothClient();
                    BluetoothDeviceInfo[] devices = client.DiscoverDevices();
                    BluetoothClient bluetoothClient = new BluetoothClient();
                    String authenticated;
                    String classOfDevice;
                    String connected;
                    String deviceAddress;
                    String deviceName;
                    String installedServices;
                    /*
                    String lastSeen;
                    String lastUsed;
                    String remembered;
                    */
                    String rssi;
                    int device_number = 0;
                    foreach (BluetoothDeviceInfo device in devices)
                    {
                        device_number++;
                        authenticated = device.Authenticated.ToString();
                        classOfDevice = device.ClassOfDevice.ToString();
                        connected = device.Connected.ToString();
                        deviceAddress = device.DeviceAddress.ToString();
                        deviceName = device.DeviceName.ToString();
                        installedServices = device.InstalledServices.ToString();
                        /*
                        lastSeen = device.LastSeen.ToString();
                        lastUsed = device.LastUsed.ToString();
                        remembered = device.Remembered.ToString();
                        */
                        rssi = device.Rssi.ToString();
                        richTextBox1.Text += "Device " + device_number.ToString() + "\nName: " + deviceName + "\nClass: " + classOfDevice + "\nAdress: " + deviceAddress + "\nSignal: " + rssi + "\nAuthenticated: " + authenticated + "\nConnected: " + connected + "\n==============================\n";
                    }
                    history_Text("Scanning for bluetooth devices completed successfully.");
                    show_ready_message = true;
                    if (device_number > 0)
                    {
                        richTextBox1.Visible = true;
                        popup_Control(richTextBox1, this.Right + 10, this.Top);
                        if (device_number > 1)
                        {
                            if (speaking_allowed)
                            {
                                speak("Scan complete. I have found " + device_number + " bluetooth devices in the area.");
                            }
                            else
                            {
                                MessageBox.Show("Scan complete. I have found " + device_number + " bluetooth devices in the area.");
                            }
                        }
                        else
                        {
                            if (speaking_allowed)
                            {
                                speak("Scan complete. I have found only " + device_number + " bluetooth device in the area.");
                            }
                            else
                            {
                                MessageBox.Show("Scan complete. I have found only " + device_number + " bluetooth device in the area.");
                            }
                        }
                    }
                    else
                    {
                        if (speaking_allowed)
                        {
                            speak("Scan complete. I couldn't find any bluetooth devices in the area.");
                        }
                        else
                        {
                            MessageBox.Show("Scan complete. I couldn't find any bluetooth devices in the area.");
                        }
                    }
                }
                catch
                {
                    await Task.Delay(100);
                    show_ready_message = true;
                    history_Text("Scan failed. Reason: bluetooth module missing or powered off. <error>");
                    pb.AppendText("Scan failed. Bluetooth module on this machine is either missing, or powered off.");
                    sr.RecognizeAsyncStop();
                    reply.Speak(pb);
                    pb.ClearContent();
                }
            }
        }

        public void buttons_AdvancedRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_AdvancedRemoteText()
        {
            button1.Text = advanced_remote_text[0];
            button2.Text = advanced_remote_text[1];
            button3.Text = advanced_remote_text[2];
            button4.Text = advanced_remote_text[3];
            button5.Text = advanced_remote_text[4];
            button6.Text = advanced_remote_text[5];
        }

        public void buttons_AskRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_AskRemoteText()
        {
            button1.Text = ask_remote_text[0];
            button2.Text = ask_remote_text[1];
        }

        public void buttons_BasicRemoteLoad()
        {
            if(buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_BasicRemoteText()
        {
            button1.Text = basic_remote_text[0];
            button2.Text = basic_remote_text[1];
            button3.Text = basic_remote_text[2];
            button4.Text = basic_remote_text[3];
            button5.Text = basic_remote_text[4];
            button6.Text = basic_remote_text[5];
            button7.Text = basic_remote_text[6];
            button8.Text = basic_remote_text[7];
            button9.Text = basic_remote_text[8];
            button10.Text = basic_remote_text[9];
        }

        public void buttons_ComputerRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_ComputerRemoteText()
        {
            button1.Text = computer_remote_text[0];
            button2.Text = computer_remote_text[1];
            button3.Text = computer_remote_text[2];
            button4.Text = computer_remote_text[3];
        }

        public void buttons_KeyloggerRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_KeyloggerRemoteText()
        {
            button1.Text = keylogger_remote_text[0];
            button2.Text = keylogger_remote_text[1];
            button3.Text = keylogger_remote_text[2];
            button4.Text = keylogger_remote_text[3];
            button5.Text = keylogger_remote_text[4];
        }

        public void buttons_SecurityRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_SecurityRemoteText()
        {
            button1.Text = security_remote_text[0];
        }

        public void buttons_SettingsRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_SettingsRemoteText()
        {
            button1.Text = settings_remote_text[0];
            button2.Text = settings_remote_text[1];
            button3.Text = settings_remote_text[2];
            button4.Text = settings_remote_text[3];
            button5.Text = settings_remote_text[4];
        }

        public void buttons_StringRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_StringRemoteText()
        {
            
        }

        public void buttons_TextboxRemoteLoad()
        {
            if (buttons_visible)
            {
                ListenBtn.Visible = true;
                ShowCPbtn.Visible = true;
                HideCPbtn.Visible = true;
                /*ACORDING TO CUSTOMIZED FUNCTIONS*/
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
            }
        }

        public void buttons_TextboxRemoteText()
        {
            button1.Text = textbox_remote_text[0];
            button2.Text = textbox_remote_text[1];
            button3.Text = textbox_remote_text[2];
            button4.Text = textbox_remote_text[3];
            button5.Text = textbox_remote_text[4];
            button6.Text = textbox_remote_text[5];
            button7.Text = textbox_remote_text[6];
        }

        public void buttons_LoadRemoteButtons()
        {
            switch(shodan_state)
            {
                case "Advanced":
                    {
                        buttons_AdvancedRemoteText();
                        buttons_AdvancedRemoteLoad();
                        break;
                    }
                case "Ask":
                    {
                        buttons_AskRemoteText();
                        buttons_AskRemoteLoad();
                        break;
                    }
                case "Basic":
                    {
                        buttons_BasicRemoteText();
                        buttons_BasicRemoteLoad();
                        break;
                    }
                case "Computer":
                    {
                        buttons_ComputerRemoteText();
                        buttons_ComputerRemoteLoad();
                        break;
                    }
                case "Keylogger":
                    {
                        buttons_KeyloggerRemoteText();
                        buttons_KeyloggerRemoteLoad();
                        break;
                    }
                case "Security":
                    {
                        buttons_SecurityRemoteText();
                        buttons_SecurityRemoteLoad();
                        break;
                    }
                case "Settings":
                    {
                        buttons_SettingsRemoteText();
                        buttons_SettingsRemoteLoad();
                        break;
                    }
                case "String":
                    {
                        buttons_StringRemoteText();
                        buttons_StringRemoteLoad();
                        break;
                    }
            }
        }

        public void change_Password()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                activate_StringCommandsGrammar();
                buttons_StringRemoteText();
                buttons_StringRemoteLoad();
                textBox1.Visible = true;
                InputButton.Visible = true;
                AbordButton.Visible = true;
                textBox1.Text = "<write password here>";
                textBox1.UseSystemPasswordChar = false;
                if (password_required == false)
                {
                    if (read_XMLData("Password") == "defaultxyz123")
                    {
                        history_Text("Assigning password protocol engaged.");
                        input_string_method = "password_assign";
                        speak("Assigning password protocol engaged.");
                    }
                    else
                    {
                        history_Text("Changing password protocol engaged.");
                        input_string_method = "password_change";
                        history_Text("Type in your old password!");
                        speak("Changing password protocol engaged. Type in your old password.");
                    }
                }
                else
                {
                    history_Text("Assigning password protocol engaged.");
                    input_string_method = "password_assign";
                    speak("This protocol requires a password assigment before any use. Please set up a password.");
                }
            }
        }

        public void change_Username()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                input_string_method = "username";
                activate_StringCommandsGrammar();
                buttons_StringRemoteText();
                buttons_StringRemoteLoad();
                textBox1.Visible = true;
                InputButton.Visible = true;
                AbordButton.Visible = true;
                textBox1.Text = "<write username here>";
                textBox1.UseSystemPasswordChar = false;
                history_Text("Changing username protocol engaged.");
                speak("Changing username protocol engaged.");
            }
        }

        public bool check_InternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void check_Password(string function)
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (password == string.Empty || password == ipx_DecryptPassString("defaultxyz123"))
                {
                    ready_for_new_command = true;
                    password_required = true;
                    change_Password();
                }
                else
                {
                    check_password_for = function;
                    input_string_method = "check_password";
                    activate_StringCommandsGrammar();
                    buttons_StringRemoteText();
                    buttons_StringRemoteLoad();
                    textBox1.Visible = true;
                    InputButton.Visible = true;
                    AbordButton.Visible = true;
                    textBox1.Text = "<write password here>";
                    textBox1.UseSystemPasswordChar = false;
                    history_Text("Password verify protocol engaged.");
                    speak("This protocol is available only after authentication. Type down your pasword.");
                }
            }
        }

        public void clear_Content()
        {
            if (ready_for_new_command)
            {
                if (!String.IsNullOrEmpty(TextBox.Text))
                {
                    history_Text("Clearing textbox's content...");
                    TextBox.Text = String.Empty;
                    speak("Textbox has been cleared.");
                }
                else
                {
                    history_Text("Clearing textbox failed. Reason: textbox already empty. <error>");
                    speak("Textbox is already empty.");
                }
            }
        }

        public void clear_Selected()
        {
            if (ready_for_new_command)
            {
                if (!String.IsNullOrEmpty(TextBox.SelectedText))
                {
                    history_Text("Clearing textbox's selected content...");
                    TextBox.SelectedText = String.Empty;
                    speak("Selected content has been cleared.");
                }
                else
                {
                    history_Text("Clearing selected content failed. Reason: no text selected. <error>");
                    speak("No text selected.");
                }
            }
        }

        public void computer_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_ComputerRemoteText();
                buttons_ComputerRemoteLoad();
                history_Text("Accessing machine commands...");
                activate_ComputerCommandsGrammar();
                speak("Machine commands enabled.");
            }
        }

        public void connect_WiFi()
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                /*if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    history_Text("Connect to WiFi protocol aborded. Reason: already connected. <error>");
                    speak("This machine is already connected to an wireless network.");
                }
                else*/
                {
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C netsh wlan connect name=%username%&pause";
                    process.StartInfo = startInfo;
                    process.Start();
                    history_Text("Connected to WiFi.");
                    speak("Connected.");
                }
            }
        }

        private async void create_Directories()
        {
            if (!Directory.Exists("Shodan"))
            {
                firsttime = true;
                create_Directory("Shodan");
            }
            await Task.Delay(100);
            if (Directory.Exists("Shodan"))
            {
                if (!Directory.Exists("Shodan/Database"))
                {
                    create_Directory("Shodan/Database");
                }
                await Task.Delay(100);
                if (!Directory.Exists("Shodan/Log"))
                {
                    create_Directory("Shodan/Log");
                }
                await Task.Delay(100);
                if (!Directory.Exists("Shodan/Network"))
                {
                    create_Directory("Shodan/Network");
                }
            }
            if(firsttime == true)
            {
                speak("It seemns this is the first time you are using the Shodan Software. I have created a folder named Shodan in current location. There I will hold all information required. Here are displayed all available commands. You should start by changing your username from settings. For that, just say change settings, then change username.");
                CommandsPannel.Visible = true;
            }
        }

        private void create_Directory(string name)
        {
            string path = name;
            try
            {
                // Determine whether the directory exists. 
                if (Directory.Exists(path))
                {
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                /*Enable full control*/
                FileSystemAccessRule fsar = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
                DirectorySecurity ds = null;
                ds = di.GetAccessControl();
                ds.AddAccessRule(fsar);
                di.SetAccessControl(ds);
                /*Enable full control*/

                // Delete the directory.
                /*di.Delete();
                Console.WriteLine("The directory was deleted successfully.");*/
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private async void create_Files()
        {
            await Task.Delay(500);
            if (!File.Exists(@"Shodan\Database\database.xml"))
            {
                FileStream fs = new FileStream(@"Shodan\Database\database.xml", FileMode.Create);
                fs.Close();
                generate_XMLDatabase();
            }
            await Task.Delay(100);
            if (!File.Exists(@"Shodan\Log\log.ipx"))
            {
                FileStream fs = new FileStream(@"Shodan\Log\log.ipx", FileMode.Create);
                fs.Close();
            }
            await Task.Delay(100);
            if (!File.Exists(@"Shodan\Log\keylog.ipx"))
            {
                FileStream fs = new FileStream(@"Shodan\Log\keylog.ipx", FileMode.Create);
                fs.Close();
            }
            await Task.Delay(100);
            if (!File.Exists(@"Shodan\Network\network.ipx"))
            {
                FileStream fs = new FileStream(@"Shodan\Network\network.ipx", FileMode.Create);
                fs.Close();
            }
            await Task.Delay(100);
            if (!File.Exists(@"Shodan\Network\location.ipx"))
            {
                FileStream fs = new FileStream(@"Shodan\Network\location.ipx", FileMode.Create);
                fs.Close();
            }
        }

        public Point cursor_GetPosition()
        {
            int x;
            int y;
            x = Cursor.Position.X;
            y = Cursor.Position.Y;
            Point cursor_coordonates = new Point(x,y);
            cursor_coordonates = this.PointToClient(new Point(x, y));
            return cursor_coordonates;
        }

        public void display_Keylogger_info()
        {
            Point tip_ballon_position = new Point();
            tip_ballon_position.Y = 257;
            tip_ballon_position.X = 367;
            label1.Location = tip_ballon_position;
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Text = "Keylogger is recording!!!";
            label1.Visible = true;
        }

        public void display_RemoteButtonInfo()
        {
            get_ButtonsBaloonTipsInfo();
            Point tip_ballon_position = new Point();
            tip_ballon_position = cursor_GetPosition();
            tip_ballon_position.Y += 18;
            label1.Location = tip_ballon_position;
            label1.Text = baloon_tip_info;
            label1.Visible = true;
        }

        public void display_Remote_Shodan_info()
        {
            Point tip_ballon_position = new Point();
            tip_ballon_position.Y = 257;
            tip_ballon_position.X = 287;
            label1.Location = tip_ballon_position;
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Text = "This machine can be remote controlled!!!";
            label1.Visible = true;
        }

        public void exit_Application()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                forcedclosed = false;
                history_Text("Application closed.                                     " + DateTime.Now.ToString("[dd/MM/yyyy]"));
                sr.RecognizeAsyncStop();
                pb.AppendText(byereply[rnd.Next(0, byereply.Length)]);
                reply.Speak(pb);
                pb.ClearContent();
                Application.Exit();
            }
        }

        public string filter_Hooked_key(string key)
        {
            switch (key)
            {
                case "Space":
                    {
                        return " ";
                    }
                case "RShiftKey":
                case "LShiftKey":
                    {
                        return " [shift] ";
                    }
                case "Return":
                    {
                        return "\n" + DateTime.Now.ToString("[HH:mm:ss]") + " " + DateTime.Now.ToString("[dd.MM.yyyy]") + "\n";
                    }
                case "RControlKey":
                case "LControlKey":
                    {
                        return " [ctrl] ";
                    }
                case "Escape":
                    {
                        return " [ESC] ";
                    }
                case "Tab":
                    {
                        return "\n";
                    }
                case "Oemcomma":
                    {
                        return ",";
                    }
                case "Decimal":
                case "OemPeriod":
                    {
                        return ".";
                    }
                case "NumPad0":
                case "D0":
                    {
                        return "0";
                    }
                case "NumPad1":
                case "D1":
                    {
                        return "1";
                    }
                case "NumPad2":
                case "D2":
                    {
                        return "2";
                    }
                case "NumPad3":
                case "D3":
                    {
                        return "3";
                    }
                case "NumPad4":
                case "D4":
                    {
                        return "4";
                    }
                case "NumPad5":
                case "D5":
                    {
                        return "5";
                    }
                case "NumPad6":
                case "D6":
                    {
                        return "6";
                    }
                case "NumPad7":
                case "D7":
                    {
                        return "7";
                    }
                case "NumPad8":
                case "D8":
                    {
                        return "8";
                    }
                case "NumPad9":
                case "D9":
                    {
                        return "9";
                    }
                case "Add":
                    {
                        return "+";
                    }
                case "Oemplus":
                    {
                        return "=";
                    }
                case "OemMinus":
                case "Subtract":
                    {
                        return "-";
                    }
                case "Multiply":
                    {
                        return "*";
                    }
                case "Divide":
                    {
                        return "/";
                    }
                case "Back":
                    {
                        return " [backspace] ";
                    }
                case "Oem1":
                    {
                        return ";";
                    }
                case "Oem5":
                    {
                        return "\\";
                    }
                case "Oem7":
                    {
                        return "'";
                    }
                case "OemQuestion":
                    {
                        return "/";
                    }
                default:
                    {
                        return key;
                    }
            }
        }

        public void generate_AdvancedCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.DarkOrchid;
                CommandsPannel.Text = "\n        Advanced Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < advancedcommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + advancedcommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_AskCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Blue;
                CommandsPannel.Text = "\n         Check Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < askcommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + askcommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_BasicCommandsList()
        {
            if (listening == true)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.MediumSpringGreen;
                CommandsPannel.Text = "\n        Basic Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < basiccommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + basiccommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_CommandsPannelCommandsList()
        {
            CommandsPannel.Text += "   " + commandspannelcommands_string[0] + "\n" + "   " + commandspannelcommands_string[1] + "\n";
        }

        public void generate_ComputerCommandsList()
        {
            if (listening == true)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.DarkOrange;
                CommandsPannel.Text = "\n      Machine Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < computercommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + computercommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_KeyloggerCommandsList()
        {
            if (listening == true)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.GreenYellow;
                CommandsPannel.Text = "\n    Keylogger Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < keyloggercommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + keyloggercommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_SecurityCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Cyan;
                CommandsPannel.Text = "\n      Security Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < securitycommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + securitycommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_SettingsCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Honeydew;
                CommandsPannel.Text = "\n      Settings Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < settingscommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + settingscommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_StringCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n      String Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < stringcommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + stringcommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        public void generate_TextboxCommandsList()
        {
            if (listening)
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Gold;
                CommandsPannel.Text = "\n      Textbox Commands:\n\n";
                generate_CommandsPannelCommandsList();
                for (int i = 0; i < textboxcommands_string.Length; i++)
                {
                    CommandsPannel.Text += "   " + textboxcommands_string[i] + "\n";
                }
            }
            else
            {
                CommandsPannel.ForeColor = System.Drawing.Color.Red;
                CommandsPannel.Text = "\n\n\n     Vocal commands are\n turned off.\n\n     The only availbale vocal\n command is \"Listen again\".";
            }
        }

        private async void generate_XMLDatabase()
        {
            await Task.Delay(100);
            xml_content = @"<?xml version = '1.0' encoding = 'utf-8'?>
<Shodan>
    <Data>
        <Password>defaultxyz123</Password>
        <Restart>false</Restart>
        <Username>defaultxyz123</Username>
    </Data>
</Shodan>";
            if (!File.Exists(@"Shodan\Database\database.xml"))
            {
                FileStream fs = new FileStream(@"Shodan\Database\database.xml", FileMode.Create);
                fs.Close();
            }
            using (StreamWriter sw = File.AppendText(@"Shodan\Database\database.xml"))
            {
                sw.WriteLine(xml_content);
            }

        }

        public void get_ButtonsBaloonTipsInfo()
        {
            switch(button_mouse_hover)
            {
                case -1:
                    {
                        baloon_tip_info = hide_CommandsPannel_info;
                        break;
                    }
                case 0:
                    {
                        baloon_tip_info = show_CommandsPannel_info;
                        break;
                    }
                case 1:
                    {   switch(shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = show_Networks_info;
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = advanced_Commands_info;
                                    break;
                                }
                            case "Computer":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = change_Username_info;
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = read_Everything_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = scan_BluetoothDevices_info;
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Computer":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = read_Selected_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Computer":
                                {
                                    baloon_tip_info = system_Info_info;
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = clear_Content_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 4:
                    {
                        switch (shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = mobile_Shodan_info;
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Computer":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = clear_Selected_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 5:
                    {
                        switch (shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Computer":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = ipx_CryptText_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 6:
                    {
                        switch (shodan_state)
                        {
                            case "Advanced":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Basic":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Keylogger":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Settings":
                                {
                                    baloon_tip_info = "No info available yet";
                                    break;
                                }
                            case "Textbox":
                                {
                                    baloon_tip_info = ipx_DecryptText_info;
                                    break;
                                }
                        }
                        break;
                    }
                case 50:
                    {
                        baloon_tip_info = start_Listening_info;
                        break;
                    }
                case 51:
                    {
                        baloon_tip_info = stop_Listening_info;
                        break;
                    }
                default :
                    {
                        baloon_tip_info = "No info available yet";
                        break;
                    }
            }
        }

        public void hello_User()
        {
            if (firsttime == false)
            {
                if (ready_for_new_command)
                {
                    ready_for_new_command = false;
                    string currenttime = DateTime.Now.ToString("HH");
                    int j;
                    Int32.TryParse(currenttime, out j);
                    if (j > 5 && j <= 10)
                    {
                        speak("Good morning " + username + ". " + readyreply[rnd.Next(0, readyreply.Length)]);
                    }
                    else if (j > 10 && j <= 19)
                    {
                        speak("Hello " + username + ". " + readyreply[rnd.Next(0, readyreply.Length)]);
                    }
                    else if (j > 19 || (j >= 0 && j < 2))
                    {
                        speak("Good evening " + username + ". " + readyreply[rnd.Next(0, readyreply.Length)]);
                    }
                    else
                    {
                        speak("Hey nightwolf.");
                    }
                }
            }
        }

        public void hide_CommandsPannel()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (CommandsPannel.Visible == true)
                {
                    history_Text("Hiding commands pannel...");
                    CommandsPannel.Visible = false;
                    speak("Commands pannel is now hidden.");
                }
                else
                {
                    history_Text("Error hiding commands pannel. Reason: already hidden. <error>");
                    speak("Commands pannel is already hidden.");
                }
            }
        }

        public void hide_RemoteButtonInfo()
        {
            label1.Visible = false;
            label1.Text = string.Empty;
            button_mouse_hover = -2;/*IN ORDER TO DISPLAY ERROR MESSAGE FOR NOT GENERATED MESSAGES*/
        }

        public void hide_Shodan()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Minimized)
                {
                    history_Text("Hidding Shodan...");
                    this.Hide();
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(2200);
                    this.ShowInTaskbar = false;
                    speak("I'm hidden.");
                }
                else
                {
                    history_Text("Hiding failed. Reeason: already hidden. <error>");
                    speak("I'm already hidden.");
                }
            }
        }

        public void history_Text(string text)
        {
            StringBuilder history_SB = new StringBuilder();
            history_SB.Append(" " + text);
            int current_StringLength;
            current_StringLength = history_SB.ToString().Length;
            for(int i = 0;i < (69 - current_StringLength);i++)
            {
                history_SB.Append(" ");
            }
            history_SB.Append(DateTime.Now.ToString("[HH:mm:ss]"));
            if (File.Exists(@"Shodan\Log\log.ipx"))
            {
                using (StreamWriter sw = File.AppendText(@"Shodan/Log/log.ipx"))
                {
                    sw.WriteLine(history_SB.ToString());
                }
            }
            history_SB.Append("\n");
            historyPannel.Text += history_SB.ToString();
            historyPannel.SelectionStart = historyPannel.Text.Length;
            historyPannel.ScrollToCaret();
            
        }
        
        public void initiate_Variables()
        {
            AbordButton.Visible = false;/*DETERMINES IF TO SHOW OR HIDE BUTTON USED IN CHANGING USERNAME FOR CANCELING PROCES*/
            baloon_tip_info = hide_CommandsPannel_info;/*USED TO DISPLAY INFO ON BUTTONS BALOON TIPS, ACCORDING TO BUTTON_MOUSE_HOVER VALUE*/
            buttons_visible = false;/*DETERMINES IF TO SHOW OR HIDE REMOTE CONTROL BUTTONs*/
            button_mouse_hover = -1;/*USED TO DETERMINE WHICH INFO TO SHOW ON BALLON TIP, ACCORDING TO MOUSE POSTION OVER A CERTAIN BUTTON (-1=>SHOW CP;0=>HIDE CP;1=>BUTTON1...)*/
            CommandsPannel.Visible = false;/*DETERMINES IF TO SHOW OR HIDE COMMANDS PANNEL*/
            firsttime = false;/*DETERMINES IF SHODAN IS USED FOR THE FIRST TIME*/
            forcedclosed = true;/*USED IN EXIT APLICATION AND FORM CLOSING FUNCTION TO WRITE IN LOG FILE HOW SHODAN WAS CLOSED: SOFT OR BRUTAL*/
            hooked_key = string.Empty;/*USED IN KEYLOGGER PROTOCOL TO HOOK PRESSED KEY*/
            hooked_log = string.Empty;/*USED IN KEYLOGGER PROTOCOL TO FILTER HOOKED PRESSED KEY*/
            InputButton.Visible = false;/*DETERMINES IF TO SHOW OR HIDE BUTTON USED IN CHANGING USERNAME*/
            input_password_stage = 0;/*USED TO DETERMINE EITHER TO WRITE PASS TO DATABES OR TO CHECK IF PASS MATCHES ON RETYPE: 0=>CHECK, 1=>WRITE TO DB*/
            input_string_method = "username";/*USED TO DETERMINE INPUT BUTTON'S FUNCTION: INPUT USERNAME OR PASSWORD*/
            keylogger_recording = false;/*USED TO DETERMINE IF KEYLLOGER IS RECORDING OR NOT*/
            label1.Visible = false;/*DETERMINES IF TO SHOW OR HIDE LABEL, USED AS TIP ICON FOR BUTTONS*/
            listening = true;/*DETERMINES IF SHODAN LISTENS TO VOCAL COMMANDS OR NOT*/
            notifyIcon.Visible = false;/*DETERMINES IF TO SHOW OR HIDE SHODAN IN SYSTEM TRAY*/
            pass_retype = string.Empty;/*USED TO CHECK IF PASSWORD MATCH ON RETYPE*/
            password_required = false;/*USED TO ASK USER TO ASSIGN A PASSWORD IN ORDER TO USE HIGH COMMANDS*/
            pictureBox2.Enabled = false;/*USED TO SHOW USER THAT MACHINE IS REMOTE CONTROLABLE BY REMOTE_SHODAN*/
            pictureBox2.Visible = false;/*USED TO SHOW USER THAT MACHINE IS REMOTE CONTROLABLE BY REMOTE_SHODAN*/
            pictureBox3.Enabled = false;/*USED TO SHOW USER THAT KEYLOGGER IS RUNNING*/
            pictureBox3.Visible = false;/*USED TO SHOW USER THAT KEYLOGGER IS RUNNING*/
            ready_for_new_command = true;/*USED TO AVOID ACTIVATING MORE THAN ONE COMMAND AT A TIME IN ASYNC MODE*/
            remote_controlled = false;/*USED TO DETERMINE IF REMOTE CONTROL IS ACTIVATED OR NOT*/
            reply.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Senior);/*DETERMINES SHODAN TO USE A FEMALE VOICE*/
            richTextBox1.Visible = false;/*USED TO SHOW OR HIDE TEXTBOX IN WHICH ARE DISPLAYED SCANNED BLUETOOTH DEVICES*/
            shodan_state = "basic";/*USED TO DETERMINE SHODAN COLOR*/
            show_ready_message = true;/*DETERMINES IF TO DISPLAY OR NOT <READY FOR NEW COMMAND> IN HISTORY PANNEL, AFTER SHODAN HAS SPOKEN*/
            speaking_allowed = true;/*DETERMINES IF SHODAN CAN SPEAK/REPLY OR CANNOT*/
            StopReadingBtn.Visible = false;/*USED TO SHOW OR HIDE BUTTON WHICH CAN STOP SHODAN FROM READING*/
            TextBox.Visible = false;/*DETERMINES IF TO SHOW OR HIDE TEXTBOX USED IN TEXTBOX COMMANDS*/
            text_box_first_time_opened = true;/*USED ON TEXTBOX MOUSE EVENT FUNCTION TO CLEAR HINT TEXT FROM TEXTBOX*/
            textBox1.Visible = false;/*DETERMINES IF TO SHOW OR HIDE TEXTBOX USED IN CHANGING USERNAME*/
            text_box1_fisrt_time_opened = true;/*USED ON TEXTBOX1 MOUSE EVENT FUNCTION TO CLEAR HINT TEXT FROM TEXTBOX1, USED IN CHANGING USERNAME*/
            timer1Tiker = 0;/*USED TO KEEP TRACK OF CURRENT IMAGE OF SHODAN: GOES FROM 0 TO 5*/
            timer1.Interval = 120;/*USED TO SET UP THE SPEED OF SHODAN'S IMAGES CHANGE RATE*/
        }

        public void initialize_Grammars()
        {
            /*ADVANCED COMANDS*/
            Choices Choiches_AdvancedCommands = new Choices();
            Choiches_AdvancedCommands.Add(advancedcommands_string);
            GrammarBuilder GB_AdvancedCommands = new GrammarBuilder();
            GB_AdvancedCommands.Append(Choiches_AdvancedCommands);
            AdvancedCommands_Grammar = new Grammar(GB_AdvancedCommands);
            AdvancedCommands_Grammar.Name = "AdvancedCommands_Grammar";
            /*ADVANCED COMANDS*/

            /*ASK COMMANDS*/
            Choices Choiches_AskCommands = new Choices();
            Choiches_AskCommands.Add(askcommands_string);
            GrammarBuilder GB_AskCommands = new GrammarBuilder();
            GB_AskCommands.Append(Choiches_AskCommands);
            AskCommands_Grammar = new Grammar(GB_AskCommands);
            AskCommands_Grammar.Name = "AskCommands_Grammar";
            /*ASK COMMANDS*/

            /*BASIC COMANDS*/
            Choices Choiches_BasicCommands = new Choices();
            Choiches_BasicCommands.Add(basiccommands_string);
            GrammarBuilder GB_BasicCommands = new GrammarBuilder();
            GB_BasicCommands.Append(Choiches_BasicCommands);
            BasicCommands_Grammar = new Grammar(GB_BasicCommands);
            BasicCommands_Grammar.Name = "BasicCommands_Grammar";
            /*BASIC COMANDS*/

            /*COMMANDS PANNEL COMMANDS*/
            Choices Choiches_CommandsPannelCommands = new Choices();
            Choiches_CommandsPannelCommands.Add(commandspannelcommands_string);
            GrammarBuilder GB_CommandsPannelCommands = new GrammarBuilder();
            GB_CommandsPannelCommands.Append(Choiches_CommandsPannelCommands);
            CommandsPannelCommands_Grammar = new Grammar(GB_CommandsPannelCommands);
            CommandsPannelCommands_Grammar.Name = "CommandsPannelCommands_Grammar";
            /*COMMANDS PANNEL COMMANDS*/

            /*COMPUTER COMMANDS*/
            Choices Choiches_ComputerCommands = new Choices();
            Choiches_ComputerCommands.Add(computercommands_string);
            GrammarBuilder GB_ComputerCommands = new GrammarBuilder();
            GB_ComputerCommands.Append(Choiches_ComputerCommands);
            ComputerCommands_Grammar = new Grammar(GB_ComputerCommands);
            ComputerCommands_Grammar.Name = "ComputerCommands_Grammar";
            /*COMPUTER COMMANDS*/

            /*KEYLOGGER COMANDS*/
            Choices Choiches_KeyLoggerCommands = new Choices();
            Choiches_KeyLoggerCommands.Add(keyloggercommands_string);
            GrammarBuilder GB_KeyLoggerCommands = new GrammarBuilder();
            GB_KeyLoggerCommands.Append(Choiches_KeyLoggerCommands);
            KeyLoggerCommands_Grammar = new Grammar(GB_KeyLoggerCommands);
            KeyLoggerCommands_Grammar.Name = "KeyLoggerCommands_Grammar";
            /*KEYLOGGER COMANDS*/

            /*LISTENING COMANDS*/
            Choices Choiches_ListeningCommands = new Choices();
            Choiches_ListeningCommands.Add(listeningcommands_string);
            GrammarBuilder GB_ListeningCommands = new GrammarBuilder();
            GB_ListeningCommands.Append(Choiches_ListeningCommands);
            Listening_Grammar = new Grammar(GB_ListeningCommands);
            Listening_Grammar.Name = "Listening_Grammar";
            /*LISTENING COMANDS*/

            /*SECURITY COMANDS*/
            Choices Choiches_SecurityCommands = new Choices();
            Choiches_SecurityCommands.Add(securitycommands_string);
            GrammarBuilder GB_SecurityCommands = new GrammarBuilder();
            GB_SecurityCommands.Append(Choiches_SecurityCommands);
            SecurityCommands_Grammar = new Grammar(GB_SecurityCommands);
            SecurityCommands_Grammar.Name = "SecurityCommands_Grammar";
            /*SETTINGS COMANDS*/

            /*SECURITY COMANDS*/
            Choices Choiches_SettingsCommands = new Choices();
            Choiches_SettingsCommands.Add(settingscommands_string);
            GrammarBuilder GB_SettingsCommands = new GrammarBuilder();
            GB_SettingsCommands.Append(Choiches_SettingsCommands);
            SettingsCommands_Grammar = new Grammar(GB_SettingsCommands);
            SettingsCommands_Grammar.Name = "SettingsCommands_Grammar";
            /*SETTINGS COMANDS*/

            /*STRING COMANDS*/
            Choices Choiches_StringCommands = new Choices();
            Choiches_StringCommands.Add(stringcommands_string);
            GrammarBuilder GB_StringCommands = new GrammarBuilder();
            GB_StringCommands.Append(Choiches_StringCommands);
            StringCommands_Grammar = new Grammar(GB_StringCommands);
            StringCommands_Grammar.Name = "StringCommands_Grammar";
            /*STRING COMANDS*/

            /*TEXTBOX COMMANDS*/
            Choices Choiches_TextboxCommands = new Choices();
            Choiches_TextboxCommands.Add(textboxcommands_string);
            GrammarBuilder GB_TextboxCommands = new GrammarBuilder();
            GB_TextboxCommands.Append(Choiches_TextboxCommands);
            TextboxCommands_Grammar = new Grammar(GB_TextboxCommands);
            TextboxCommands_Grammar.Name = "TextboxCommands_Grammar";
            /*TEXTBOX COMMANDS*/

            /*LOAD GRAMMAR INTO SPEECH RECOGNITION => ALLOWS COMMANDS LISTED IN GRAMMAR TO BE USED*/
            sr.LoadGrammar(AdvancedCommands_Grammar);
            sr.LoadGrammar(AskCommands_Grammar);
            sr.LoadGrammar(BasicCommands_Grammar);
            sr.LoadGrammar(CommandsPannelCommands_Grammar);
            sr.LoadGrammar(ComputerCommands_Grammar);
            sr.LoadGrammar(KeyLoggerCommands_Grammar);
            sr.LoadGrammar(Listening_Grammar);
            sr.LoadGrammar(SecurityCommands_Grammar);
            sr.LoadGrammar(SettingsCommands_Grammar);
            sr.LoadGrammar(StringCommands_Grammar);
            sr.LoadGrammar(TextboxCommands_Grammar);
            /*LOAD GRAMMAR INTO SPEECH RECOGNITION*/

            /*SET A CERTAIN GRAMMAR ENABLED OR DISABLE => IMPROVE SHODAN'S CHANCE OF SUCCES BY DROPPING THE RATE OF CONFUSING COMMANDS*/
            sr.Grammars[sr.Grammars.IndexOf(AdvancedCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(AskCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(BasicCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(CommandsPannelCommands_Grammar)].Enabled = true;
            sr.Grammars[sr.Grammars.IndexOf(ComputerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(KeyLoggerCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(Listening_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SecurityCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(SettingsCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(StringCommands_Grammar)].Enabled = false;
            sr.Grammars[sr.Grammars.IndexOf(TextboxCommands_Grammar)].Enabled = false;
            /*SET A CERTAIN GRAMMAR ENABLED OR DISABLE*/

            sr.SetInputToDefaultAudioDevice();/*SETS DEFAULT MICROPHONE AS USED DEVICE*/
            sr.SpeechRecognized += sr_SpeechRecognized;
            sr.RecognizeAsync(RecognizeMode.Multiple);/*STARTS RECOGNITION*/

            reply.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(synth_SpeakCompleted);/*USED TO DISABLE SPEECH RECOGNITION ENGINE WHILE SPEAKING*/
        }
        
        private void InputButton_Click(object sender, EventArgs e)
        {
            input_String();
        }

        public void input_PasswordAssign()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (input_password_stage == 0)
                {
                    if (textBox1.Text != string.Empty && textBox1.Text != "<write password here>")
                    {
                        if (textBox1.Text.Length < 8)
                        {
                            history_Text("Assign password protocol failed. Reason: password too short. <error>");
                            speak("Password must contain at least 8 characters.");
                            textBox1.Text = string.Empty;
                        }
                        else if (textBox1.Text.Length > 16)
                        {
                            history_Text("Assign password protocol failed. Reason: password too long. <error>");
                            speak("Password cannot be longer than 16 characters.");
                            textBox1.Text = string.Empty;
                        }
                        else
                        {
                            pass_retype = textBox1.Text;
                            input_password_stage = 1;
                            textBox1.Text = string.Empty;
                            history_Text("Retype password!");
                            speak("Retype password.");
                        }
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                        history_Text("Input password protocol failed. Reason: empty string. <error>");
                        speak("Password cannot be an empty string.");
                    }
                }
                else if (input_password_stage == 1)
                {
                    if (pass_retype == textBox1.Text)
                    {
                        write_XMLData("Password", ipx_CryptPassString(textBox1.Text));
                        history_Text("Password assigned.");
                        speak("Password has been successfully assigned.");
                        InputButton.Visible = false;
                        AbordButton.Visible = false;
                        textBox1.Visible = false;
                        password_required = false;
                        textBox1.Text = "<write password here>";
                        text_box1_fisrt_time_opened = true;
                        activate_SettingsCommandsGrammar();
                        buttons_SettingsRemoteText();
                        buttons_SettingsRemoteLoad();
                        password = ipx_DecryptPassString(read_XMLData("Password"));
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                        history_Text("Input password protocol failed. Reason: no match. <error>");
                        speak("Password don't match.");
                    }
                    input_password_stage = 0;
                }
            }
        }

        public void input_PasswordChange()
        {
            if(ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if(input_password_stage == 0)
                {
                    if(textBox1.Text != password)
                    {
                        history_Text("Changing password protocol failed. Reason: password don't match.");
                        speak("Password doesn't match.");
                    }
                    else
                    {
                        input_password_stage = 1;
                        history_Text("Type in the new password.");
                        speak("Type in the new password.");
                    }
                    textBox1.Text = string.Empty;
                }
                else if (input_password_stage == 1)
                {
                    if (textBox1.Text != string.Empty && textBox1.Text != "<write password here>")
                    {
                        if (textBox1.Text.Length < 8)
                        {
                            history_Text("Assign password protocol failed. Reason: password too short. <error>");
                            speak("Password must contain at least 8 characters.");
                            textBox1.Text = string.Empty;
                        }
                        else if (textBox1.Text.Length > 16)
                        {
                            history_Text("Assign password protocol failed. Reason: password too long. <error>");
                            speak("Password cannot be longer than 16 characters.");
                            textBox1.Text = string.Empty;
                        }
                        else
                        {
                            pass_retype = textBox1.Text;
                            input_password_stage = 2;
                            textBox1.Text = string.Empty;
                            history_Text("Retype password!");
                            speak("Retype password.");
                        }
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                        history_Text("Input password protocol failed. Reason: empty string. <error>");
                        speak("Password cannot be an empty string.");
                    }
                }
                else if (input_password_stage == 2)
                {
                    if (pass_retype == textBox1.Text)
                    {
                        write_XMLData("Password", ipx_CryptPassString(textBox1.Text));
                        history_Text("Password successfully changed.");
                        speak("Password has been successfully changed.");
                        InputButton.Visible = false;
                        AbordButton.Visible = false;
                        textBox1.Visible = false;
                        textBox1.Text = "<write password here>";
                        text_box1_fisrt_time_opened = true;
                        activate_SettingsCommandsGrammar();
                        buttons_SettingsRemoteText();
                        buttons_SettingsRemoteLoad();
                        password = ipx_DecryptPassString(read_XMLData("Password"));
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                        history_Text("Input password protocol failed. Reason: no match. <error>");
                        history_Text("Type in your old password.");
                        speak("Password don't match. Type in your old password.");
                    }
                    input_password_stage = 0;
                }
            }
        }

        public void input_PasswordCheck()
        {
            if (ready_for_new_command == true)
            {
                if (textBox1.Text == password)
                {
                    history_Text("Password accepted.");
                    textBox1.Text = string.Empty;
                    InputButton.Visible = false;
                    AbordButton.Visible = false;
                    textBox1.Visible = false;
                    text_box1_fisrt_time_opened = true;
                    switch (check_password_for)
                    {
                        case "say_time":
                            {
                                say_Time();
                                activate_SettingsCommandsGrammar();
                                buttons_SettingsRemoteText();
                                buttons_SettingsRemoteLoad();
                                break;
                            }
                        case "show_log":
                            {
                                show_Log();
                                activate_SettingsCommandsGrammar();
                                buttons_SettingsRemoteText();
                                buttons_SettingsRemoteLoad();
                                break;
                            }
                        case "keylogger":
                            {
                                keyLogger_Commands();
                                break;
                            }
                    }
                }
                else
                {
                    textBox1.Text = string.Empty;
                    history_Text("Incorrect password.");
                    speak("Incorrect password.");
                }
            }
        }

        public void input_String()
        {
            switch(input_string_method)
            {
                case "username":
                    {
                        input_Username();
                        break;
                    }
                case "password_assign":
                    {
                        input_PasswordAssign();
                        break;
                    }
                case "password_change":
                    {
                        input_PasswordChange();
                        break;
                    }
                case "check_password":
                    {
                        input_PasswordCheck();
                        break;
                    }
            }
        }

        public void input_Username()
        {
            if(ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (textBox1.Text != string.Empty && textBox1.Text != "<write username here>")
                {
                    bool default_user = false;
                    if(read_XMLData("Username") == "defaultxyz123")
                    {
                        default_user = true;
                    }
                    write_XMLData("Username", textBox1.Text);
                    if (firsttime == true || default_user == true)
                    {
                        history_Text("First user registered as " + textBox1.Text + ".");
                        speak("Hello " + textBox1.Text + ". Nice to meet you. What is your next task?");
                    }
                    else
                    {
                        history_Text("Username changed to " + textBox1.Text + ".");
                        speak("You have successfully changed your username to " + textBox1.Text + ".");
                    }
                    activate_SettingsCommandsGrammar();
                    buttons_SettingsRemoteText();
                    buttons_SettingsRemoteLoad();
                    InputButton.Visible = false;
                    AbordButton.Visible = false;
                    textBox1.Visible = false;
                    textBox1.Text = "<write username here>";
                    text_box1_fisrt_time_opened = true;
                }
                else
                {
                    textBox1.Text = string.Empty;
                    history_Text("Input username protocol failed. Reason: empty string. <error>");
                    speak("Username cannot be an empty string.");
                }
            }
        }

        private String ipx_CryptString(String STRING_TO_BE_CRYPTED)
        {
            string cryptstring0 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring1 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring2 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring3 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring4 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring5 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring6 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring7 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring8 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring9 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring10 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring11 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring12 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring13 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring14 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring15 = "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            if (!String.IsNullOrEmpty(STRING_TO_BE_CRYPTED))
            {
                if (STRING_TO_BE_CRYPTED.Length <= 250)
                {
                    String text_to_be_encrypted;
                    String chosen_scrypt_string;
                    StringBuilder encrypted_text_SB = new StringBuilder();
                    char generatedchar;
                    char char_from_notcrypted_text;
                    char char_from_cryptyngscrypt;
                    int encryption_script_method = rnd.Next(0, cryptstrings.Length);
                    int counter = 0;
                    text_to_be_encrypted = STRING_TO_BE_CRYPTED;
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    encrypted_text_SB.Append((char)('z' + encryption_script_method));
                    for (int i = 0; i < text_to_be_encrypted.Length; i++)
                    {
                        char_from_notcrypted_text = text_to_be_encrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_notcrypted_text + (int)(char_from_cryptyngscrypt) - 25);
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    encrypted_text_SB.Append(random_Char());
                    encrypted_text_SB.Append(random_Char());
                    history_Text("Encrypting text finished successfully.");
                    speak("Text has been encrypted successfully.");
                    return encrypted_text_SB.ToString();
                }
                else
                {
                    history_Text("Encrypting text has failed. Reason: text too long. <error>");
                    speak("Encryption failed. Can't encrypt texts longer than 250 characters.");
                    return STRING_TO_BE_CRYPTED;
                }
            }
            else
            {
                history_Text("Encrypting text has failed. Reason: empty string. <error>");
                speak("Encryption failed. Can't encrypt empty strings.");
                return string.Empty;
            }
        }

        private String ipx_DecryptString(String STRING_TO_BE_DECRYPTED)
        {
            string cryptstring0 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring1 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring2 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring3 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring4 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring5 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring6 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring7 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring8 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring9 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring10 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring11 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring12 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring13 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring14 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring15 = "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            char char_from_crypted_text;
            char char_from_cryptyngscrypt;
            char generatedchar;
            String text_to_be_decrypted;
            StringBuilder decrypted_text = new StringBuilder();
            String chosen_scrypt_string;
            int encryption_script_method;
            int counter = 0;
            if (!String.IsNullOrEmpty(STRING_TO_BE_DECRYPTED))
            {
                if (STRING_TO_BE_DECRYPTED.Length <= 753)
                {
                    text_to_be_decrypted = STRING_TO_BE_DECRYPTED;
                    encryption_script_method = (int)(text_to_be_decrypted[0] - 'z');
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    for (int i = 3; i < text_to_be_decrypted.Length - 1; i += 3)
                    {
                        char_from_crypted_text = text_to_be_decrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_crypted_text - (int)(char_from_cryptyngscrypt) + 25);
                        decrypted_text.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    history_Text("Decrypting text finished successfully.");
                    speak("Text has been decrypted successfully.");
                    return decrypted_text.ToString();
                }
                else
                {
                    history_Text("Decrypting text has failed. Reason: text too long. <error>");
                    speak("Decryption protocol failed. Can't decrypt texts this long.");
                    return STRING_TO_BE_DECRYPTED;
                }
            }
            else
            {
                history_Text("Decrypting text has failed. Reason: empty string. <error>");
                speak("Decryption protocol failed. Can't decrypt empty string.");
                return string.Empty;
            }
        }

        private String ipx_CryptPassString(string STRING_TO_BE_CRYPTED)
        {
            string cryptstring15 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring14 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring13 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring12 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring11 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring10 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring9 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring8 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring7 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring6 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring5 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring4 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring3 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring2 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring1 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring0= "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            String text_to_be_encrypted;
            String chosen_scrypt_string;
            StringBuilder encrypted_text_SB = new StringBuilder();
            char generatedchar;
            char char_from_notcrypted_text;
            char char_from_cryptyngscrypt;
            int encryption_script_method = rnd.Next(0, cryptstrings.Length);
                    int counter = 0;
                    text_to_be_encrypted = STRING_TO_BE_CRYPTED;
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    encrypted_text_SB.Append((char)('z' + encryption_script_method));
                    for (int i = 0; i < text_to_be_encrypted.Length; i++)
                    {
                        char_from_notcrypted_text = text_to_be_encrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_notcrypted_text + (int)(char_from_cryptyngscrypt) - 25);
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    encrypted_text_SB.Append(random_Char());
                    encrypted_text_SB.Append(random_Char());
                    return encrypted_text_SB.ToString();
                }

        private String ipx_DecryptPassString(string STRING_TO_BE_DECRYPTED)
        {
            string cryptstring15 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring14 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring13 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring12 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring11 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring10 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring9 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring8 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring7 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring6 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring5 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring4 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring3 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring2 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring1 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring0 = "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            char char_from_crypted_text;
            char char_from_cryptyngscrypt;
            char generatedchar;
            String text_to_be_decrypted;
            StringBuilder decrypted_text = new StringBuilder();
            String chosen_scrypt_string;
            int encryption_script_method;
            int counter = 0;
                    text_to_be_decrypted = STRING_TO_BE_DECRYPTED;
                    encryption_script_method = (int)(text_to_be_decrypted[0] - 'z');
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    for (int i = 3; i < text_to_be_decrypted.Length - 1; i += 3)
                    {
                        char_from_crypted_text = text_to_be_decrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_crypted_text - (int)(char_from_cryptyngscrypt) + 25);
                        decrypted_text.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    return decrypted_text.ToString();
                }

        private void ipx_CryptText()
        {
            if(ready_for_new_command == true)
            {
                ready_for_new_command = false;
                history_Text("Initiating crypting protocol...");
                if (text_box_first_time_opened == true)
                {
                    text_box_first_time_opened = false;
                }
                TextBox.Text = ipx_CryptString(TextBox.Text);
            }
        }

        private void ipx_DecryptText()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                history_Text("Initiating decrypting protocol...");
                if (text_box_first_time_opened == true)
                {
                    text_box_first_time_opened = false;
                }
                TextBox.Text = ipx_DecryptString(TextBox.Text);
            }
        }

        public void keyLogger_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_KeyloggerRemoteText();
                buttons_KeyloggerRemoteLoad();
                history_Text("Accessing key logger commands...");
                if (TextBox.Visible == true)
                {
                    TextBox.Visible = false;
                }
                activate_KeyloggerCommandsGrammar();
                speak("Key logger is ready to use.");
            }
        }

        public void keylogger_StartRecording()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (keylogger_recording == false)
                {
                    hook = new UserActivityHook();
                    hook.KeyUp += (s, e) =>
                    {
                        hooked_key = e.KeyData.ToString();
                        hooked_log = filter_Hooked_key(hooked_key);
                        System.IO.File.AppendAllText(@"Shodan\Log\keylog.ipx", hooked_log);
                    };
                    keylogger_recording = true;
                    pictureBox3.Enabled = true;
                    pictureBox3.Visible = true;
                    history_Text("Keylogger has start recording.");
                    speak("Keylogger has start recording.");
                }
                else
                {
                    history_Text("Couldn't start keylog recording. Reason: already running. <error>");
                    speak("Key logger is already recording.");
                }
            }
        }

        public void keylogger_StopRecording()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (keylogger_recording == true)
                {
                    hook.Stop();
                    keylogger_recording = false;
                    pictureBox3.Enabled = false;
                    pictureBox3.Visible = false;
                    history_Text("Keylogger has stopped recording.");
                    speak("Keylogger has stopped recording.");
                }
                else
                {
                    history_Text("Couldn't stop keylog recording. Reason: already stopped. <error>");
                    speak("Key logger is already stopped.");
                }
            }
        }

        public void keylogger_ClearLog()
        {
            if(ready_for_new_command == true)
            {
                ready_for_new_command = false;
                System.IO.File.WriteAllText(@"Shodan\Log\keylog.ipx", string.Empty);
                activate_KeyloggerCommandsGrammar();
                buttons_KeyloggerRemoteText();
                buttons_KeyloggerRemoteLoad();
                history_Text("Key log register has been cleared.");
                speak("Key log register has been cleared.");
            }
        }

        private async void log_Stamp()
        {
            if (!firsttime)
            {
                using (StreamWriter sw = File.AppendText(@"Shodan/Log/log.ipx"))
                {
                    sw.WriteLine("");
                    sw.WriteLine("================================================================================");
                    sw.WriteLine("");
                    sw.WriteLine("New log session");
                    sw.WriteLine("");
                    sw.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy"));
                    sw.WriteLine("Time: " + DateTime.Now.ToString("[HH:mm:ss]"));
                    sw.WriteLine("User: " + username);
                    sw.WriteLine("Commands log:");
                }
            }
            else
            {
                await Task.Delay(1000);
                try
                {
                    using (StreamWriter sw = File.AppendText(@"Shodan/Log/log.ipx"))
                    {
                        sw.WriteLine("First log session");
                        sw.WriteLine("");
                        sw.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy"));
                        sw.WriteLine("Time: " + DateTime.Now.ToString("[HH:mm:ss]"));
                        sw.WriteLine("Commands log:");
                    }
                }
                catch(IOException)
                {
                    historyPannel.Text = "INTERNAL ERROR!!! RESTART APPLICATION!";
                }
            }

        }

        public void minimize_Shodan()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (WindowState == FormWindowState.Normal)
                {
                    history_Text("Minimizing Shodan...");
                    WindowState = FormWindowState.Minimized;
                    speak("I'm down");
                }
                else
                {
                    history_Text("Minimizing failed. Reeason: already minimized. <error>");
                    speak("I'm already minimized.");
                }
            }
        }

        public void mobile_Shodan()
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Initializing remote control...");
                if (remote_controlled == false)
                {
                    if (check_InternetConnection() == true)
                    {
                        pictureBox2.Enabled = true;
                        pictureBox2.Visible = true;
                        history_Text("Remote control has been enabled.");
                        speak("Remote control has been enabled.");
                        remote_controlled = true;
                        timer2.Start();
                    }
                    else
                    {
                        history_Text("Remote control didn't start. Reason: no internet connection. <error>");
                        speak("Remote control protocol requires a functional internet connection!");
                    }
                }
                else
                {
                    history_Text("Couldn't engage remote control. Reason: already running. <error>");
                    speak("Remote control protocol is already running.");
                }
            }
        }

        public void mobile_Shodan_disable()
        {
            if (ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if (remote_controlled == true)
                {
                    timer2.Stop();
                    ResetwebBrowser.Navigate("http://scorpionipx.comze.com/WR_set_command_received_false.html");
                    history_Text("Remote control has been disabled.");
                    pictureBox2.Enabled = false;
                    pictureBox2.Visible = false;
                    remote_controlled = false;
                    speak("Remote control has been disabled.");
                }
                else
                {
                    history_Text("Couldn't stop remote control. Reason: already stopped. <error>");
                    speak("Remote control protocol is already stopped.");
                }
            }
        }

        public void mobile_Shodan_return_new_command()
        {
            timer2.Stop();
            String mobile_Shodan_command;
            mobile_Shodan_command = return_FileContent("http://www.scorpionipx.comze.com/RS_command_to_execute.html");
            mobile_Shodan_command = mobile_Shodan_command.Substring(0,7);
            mobile_Shodan_exectute_command(mobile_Shodan_command);
        }

        public String mobile_Shodan_read_message()
        {
            String message;
            int index;
            message = return_FileContent("http://www.scorpionipx.comze.com/message_string.html");
            index = message.IndexOf("\n");
            message = message.Substring(0, index);
            return message;
        }

        public void mobile_Shodan_speak_message(String MESSAGE)
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Remote vocal message received.");
                speak(MESSAGE);
            }
        }

        public void mobile_Shodan_exectute_command(String COMMAND)
        {
            switch(COMMAND)
            {
                case "0001_ms":
                    {
                        mobile_Shodan_reset();
                        say_Time();
                        break;
                    }
                case "0002_ms":
                    {
                        mobile_Shodan_reset();
                        break;
                    }
                case "0004_ms":
                    {
                        mobile_Shodan_reset();
                        String received_message = mobile_Shodan_read_message();
                        mobile_Shodan_speak_message(received_message);
                        break;
                    }
                case "0005_ms":
                    {
                        mobile_Shodan_reset();
                        history_Text("Remote text message received.");
                        String received_message = mobile_Shodan_read_message();
                        MessageBox.Show(received_message);
                        break;
                    }
                case "0008_ms":
                    {
                        mobile_Shodan_disable();
                        break;
                    }
                case "0009_ms":
                    {
                        mobile_Shodan_reset();
                        history_Text("Closed by a remote device.");
                        exit_Application();
                        break;
                    }
                case "0011_ms":
                    {
                        mobile_Shodan_reset();
                        mobile_Shodan_gain_control();
                        break;
                    }
                        default:
                    {
                        mobile_Shodan_reset();
                        speak("online command received.");
                        break;
                    }

            }
        }

        public async void mobile_Shodan_reset()
        {
            ResetwebBrowser.Navigate("http://scorpionipx.comze.com/WR_set_command_received_false.html");
            await Task.Delay(2000);
            timer2.Start();
        }

        String mobile_Shodan_rdy_for_command;

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                mobile_Shodan_rdy_for_command = return_FileContent("http://www.scorpionipx.comze.com/RS_command_received.html");
            }
            catch
            {
                mobile_Shodan_disable();
            }
            if(mobile_Shodan_rdy_for_command.Contains("1"))
            {
                mobile_Shodan_return_new_command();
            }
        }

        public void mobile_Shodan_gain_control()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(0, 0);
            Cursor.Hide();
        }

        public void open_CD_Tray()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Opening disk tray...");
                CDTray.SendCDCommand("set CDAudio door open");
                speak("Ready to load.");
            }
        }

        public void popup_Control(Control control_to_pop, int X_position, int Y_position)
        {
            PopupWindow popup = new PopupWindow(control_to_pop);
            popup.Show(new Point(X_position, Y_position));
        }

        public char random_Char()
        {
            char randomchar;
            randomchar = (char)('a' + rnd.Next(0, 100));
            return randomchar;
        }

        public void read_Everything()
        {
            if (ready_for_new_command)
            {
                if (!String.IsNullOrEmpty(TextBox.Text))
                {
                    bool hadToEnable = false;
                    if (!speaking_allowed)
                    {
                        speaking_allowed = true;
                        hadToEnable = true;
                    }
                    history_Text("Reading whole text...");
                    StopReadingBtn.Visible = true;
                    speak(TextBox.Text);
                    if (hadToEnable)
                    {
                        speaking_allowed = false;
                    }
                }
                else
                {
                    history_Text("Reading text failed! Reason: textbox is empty. <error>");
                    speak("Textbox is empty");
                }
            }
        }

        public void read_Password()
        {
            if(!File.Exists(@"Shodan\Database\database.xml"))
            {
                password = string.Empty;
            }
            else
            {
                password = ipx_DecryptPassString(read_XMLData("Password"));
            }
        }

        public void read_Selected()
        {
            if (ready_for_new_command)
            {
                if (!String.IsNullOrEmpty(TextBox.SelectedText))
                {
                    bool hadToEnable = false;
                    if (!speaking_allowed)
                    {
                        speaking_allowed = true;
                        hadToEnable = true;
                    }
                    history_Text("Reading selected text...");
                    StopReadingBtn.Visible = true;
                    speak(TextBox.SelectedText);
                    if (hadToEnable)
                    {
                        speaking_allowed = false;
                    }
                }
                else
                {
                    history_Text("Reading selected text failed! Reason: no text selected. <error>");
                    speak("There is no text selected.");
                }
            }
        }

        public void read_Username()
        {
            if(firsttime == true)
            {
                username = "User";
            }
            else
            {
                if(read_XMLData("Username") == "defaultxyz123")
                {
                    username = "User";
                }
                else
                {
                    username = read_XMLData("Username");
                }
            }
        }

        private string read_XMLData(string element)
        {
            String xmlString = System.IO.File.ReadAllText(@"Shodan/Database/database.xml");
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing(element);
                string output_element = reader.ReadElementContentAsString();
                return output_element;
            }
        }

        public void ready_Message()
        {
            historyPannel.Text += "Ready to receive a new command!\n";
            historyPannel.SelectionStart = historyPannel.Text.Length;
            historyPannel.ScrollToCaret();
        }

        public void restart_Computer()
        {
            if (ready_for_new_command)
            {
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C shutdown -r -t 30 -c \"Restarted by Shodan\"";
                process.StartInfo = startInfo;
                process.Start();
                history_Text("Restarting machine...");
                speak("Restarting machine.");
                exit_Application();
            }
        }

        public String return_FileContent(String URL)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(URL);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            return content;
        }

        public void say_Time()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                bool hadToEnable = false;
                if (!speaking_allowed)
                {
                    speaking_allowed = true;
                    hadToEnable = true;
                }
                history_Text("Telling time...");
                string hour;
                string minutes;
                hour = DateTime.Now.ToString("hh");
                if (hour[0] == '0')
                {
                    hour = hour.Substring(1,1);
                }
                minutes = DateTime.Now.ToString("mm");
                if(minutes[0] == '0')
                {
                    minutes = minutes.Substring(1, 1);
                }
                if (minutes != string.Empty)
                {
                    if(minutes == "1")
                    {
                        speak("It's " + hour + " and " + minutes + " minute " + DateTime.Now.ToString("tt"));
                    }
                    else
                    {
                        speak("It's " + hour + " and " + minutes + " minutes " + DateTime.Now.ToString("tt"));
                    }
                }
                else
                {
                    if (DateTime.Now.ToString("tt") == "PM")
                    {
                        speak("It's midnight.");
                    }
                    else
                    {
                        speak("It's " + hour + " o'clock.");
                    }
                }
                if (hadToEnable)
                {
                    speaking_allowed = false;
                }
            }
        }

        public void security_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_SecurityRemoteText();
                buttons_SecurityRemoteLoad();
                history_Text("Accessing security commands...");
                activate_SecurityCommandsGrammar();
                speak("Accessing security commands.");
            }
        }

        public void settings_Commands()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                buttons_SettingsRemoteText();
                buttons_SettingsRemoteLoad();
                history_Text("Accessing settings commands...");
                activate_SettingsCommandsGrammar();
                speak("Accessing settings commands.");
            }
        }

        public void shodan_State_Advanced()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.m0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.m1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.m2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.m3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.m4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.m5;
                        break;
                    }
            }
        }

        public void shodan_State_Ask()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.a0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.a1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.a2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.a3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.a4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.a5;
                        break;
                    }
            }
        }

        public void shodan_State_Cyan()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.s0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.s1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.s2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.s3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.s4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.s5;
                        break;
                    }
            }
        }

        public void shodan_State_Green()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.g__1_;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.g__2_;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.g__3_;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.g__4_;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.g__5_;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.g__6_;
                        break;
                    }
            }
        }

        public void shodan_State_Keylogger()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.k0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.k1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.k2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.k3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.k4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.k5;
                        break;
                    }
            }
        }

        public void shodan_State_Orange()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.o0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.o1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.o2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.o3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.o4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.o5;
                        break;
                    }
            }
        }

        public void shodan_State_Red()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.red0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.red1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.red2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.red3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.red4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.red5;
                        break;
                    }
            }
        }

        public void shodan_State_Yellow()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.y0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.y1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.y2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.y3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.y4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.y5;
                        break;
                    }
            }
        }

        public void shodan_State_Settings()
        {
            switch (timer1Tiker)
            {
                case 0:
                    {
                        pictureBox1.Image = Properties.Resources.set0;
                        break;
                    }
                case 1:
                    {
                        pictureBox1.Image = Properties.Resources.set1;
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Image = Properties.Resources.set2;
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Image = Properties.Resources.set3;
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Image = Properties.Resources.set4;
                        break;
                    }
                default:
                    {
                        pictureBox1.Image = Properties.Resources.set5;
                        break;
                    }
            }
        }

        public void show_CommandsPannel()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (CommandsPannel.Visible == true)
                {
                    history_Text("Error showing commands pannel. Reason: already shown. <error>");
                    speak("Commands pannel is already shown.");
                }
                else
                {
                    history_Text("Showing commands pannel...");
                    CommandsPannel.Visible = true;
                    speak("Commands pannel is now visible.");
                }
            }
        }

        public void show_KeyLog()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                if (new FileInfo("Shodan\\Log\\keylog.ipx").Length == 0)
                {
                    history_Text("There is no record in the key log register.");
                    speak("There is no record in the key log register.");
                }
                else
                {
                    history_Text("Accessing keylog data...");
                    speak("Accessing key log data...");
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C title ~Shodan~     Shodan keylog registry     ~Shodan~&color a&cls&type Shodan\\Log\\keylog.ipx&echo.&pause";
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
        }

        public void show_Log()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Accessing log data...");
                speak("Accessing log data...");
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C title ~Shodan~     Shodan log registry     ~Shodan~&color a&cls&type Shodan\\Log\\log.ipx&pause";
                process.StartInfo = startInfo;
                process.Start();
            }
        }

        private async void show_Networks()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Scanning networks protocol engaged...");
                string availablenetworks;
                if (!File.Exists(@"Shodan\Network\network.ipx"))
                {
                    FileStream fs = new FileStream(@"Shodan\Network\network.ipx", FileMode.Create);
                    fs.Close();
                }
                await Task.Delay(50);
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C color a&cls&netsh wlan show network mode=bssid >\"Shodan\\Network\\network.ipx\"&pause";
                process.StartInfo = startInfo;
                process.Start();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C title ~Shodan~     WiFi Networks     ~Shodan~&color a&cls&netsh wlan show network mode=bssid&pause";
                process.StartInfo = startInfo;
                process.Start();
                await Task.Delay(1000);
                if (File.Exists(@"Shodan\Network\network.ipx"))
                {
                    try
                    {
                        StreamReader readtext = new StreamReader(@"Shodan\Network\network.ipx");
                        StringBuilder networkSB = new StringBuilder();
                        availablenetworks = readtext.ReadLine();
                        availablenetworks = readtext.ReadLine();
                        availablenetworks = readtext.ReadLine();
                        if (availablenetworks != "The wireless local area network interface is powered down and doesn't support the requested operation.")
                        {
                            networkSB.Append("Scan complete. " + availablenetworks + "The most powerfull network seems to be ");
                            availablenetworks = readtext.ReadLine();
                            availablenetworks = readtext.ReadLine();
                            networkSB.Append(availablenetworks + ". Power ");
                            availablenetworks = readtext.ReadLine();
                            availablenetworks = readtext.ReadLine();
                            availablenetworks = readtext.ReadLine();
                            availablenetworks = readtext.ReadLine();
                            availablenetworks = readtext.ReadLine();
                            networkSB.Append(availablenetworks + ".");
                            readtext.Close();
                            history_Text("Networks scan completed successfully.");
                        }
                        else
                        {
                            history_Text("LAN scan failed. Reason: WI-FI turned off or unavailable. <error>");
                            networkSB.Append(availablenetworks);
                        }
                        speak(networkSB.ToString());
                    }
                    catch (IOException)
                    {
                        history_Text("Failed to read net data. Reason: file used in other thread. <error>");
                        speak("Unable to read data.");
                    }
                }
            }
    }

        public void show_Shodan()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                notifyIcon.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                history_Text("Showing Shodan...");
                speak("I'm here.");
            }
        }

    public void show_Textbox()
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                if(TextBox.Visible == false)
                {
                    history_Text("Opening textbox...");
                    buttons_TextboxRemoteText();
                    buttons_TextboxRemoteLoad();
                    activate_TextboxCommandsGrammar();
                    TextBox.Visible = true;
                    speak("Textbox is ready to use.");
                }
            }
        }

        public void simulate_KeyPressing(string keypressed)
        {
            SendKeys.Send(keypressed);
        }

        public void speak(string speech)
        {
            if (speaking_allowed)
            {
                sr.RecognizeAsyncStop();
                pb.AppendText(speech);
                reply.SpeakAsync(pb);
                pb.ClearContent();
            }
            else
            {
                ready_for_new_command = true;
                ready_Message();
            }
        }

        public void start_Listening()
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                if(listening == true)
                {
                    history_Text("Failed to enable vocal commands. Reason: already enabled. <error>");
                    speak("Vocal commands are alredy enabled.");
                }
                else
                {
                    listening = true;
                    ListenBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
                    history_Text("Vocal commands enabled.");
                    switch (shodan_state)
                    {
                        case "Advanced":
                            {
                                activate_AdvancedCommandsGrammar();
                                break;
                            }
                        case "Basic":
                            {
                                activate_BasicCommandsGrammar();
                                break;
                            }
                        case "Security":
                            {
                                activate_SecurityCommandsGrammar();
                                break;
                            }
                        case "Settings":
                            {
                                activate_SettingsCommandsGrammar();
                                break;
                            }
                        case "String":
                            {
                                activate_StringCommandsGrammar();
                                break;
                            }
                        case "Textbox":
                            {
                                activate_TextboxCommandsGrammar();
                                break;
                            }
                    }
                            speak("Vocal commands are now available.");
                }
            }
        }

        public void stop_Listening()
        {
            if(ready_for_new_command)
            {
                ready_for_new_command = false;
                if(listening == true)
                {
                    listening = false;
                    ListenBtn.BackColor = System.Drawing.Color.Maroon;
                    history_Text("Vocal commands diabled.");
                    activate_Listening_Grammar();
                    switch (shodan_state)
                    {
                        case "Advanced":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                        case "Basic":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                        case "Security":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                        case "Settings":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                        case "String":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                        case "Textbox":
                            {
                                generate_BasicCommandsList();
                                break;
                            }
                    }
                    speak("Vocal commands are no longer available.");
                }
                else
                {
                    history_Text("Failed to disable vocal commands. Reason: already disbled. <error>");
                    speak("Vocal commands are alredy diabled.");
                }
            }
        }

        public void stop_Reading()
        {
            history_Text("Stopped reading.");
            reply.SpeakAsyncCancelAll();
        }

        public void switch_Reply()
        {
            if(ready_for_new_command == true)
            {
                ready_for_new_command = false;
                if(speaking_allowed == true)
                {
                    history_Text("Disableing Shodan's reply...");
                    speak("Replies are now disabled.");
                    speaking_allowed = false;
                }
                else
                {
                    history_Text("Enableing Shodan's reply...");
                    speaking_allowed = true;
                    speak("Replies are now activated.");
                }
            }
        }

        public void synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            try
            {
                sr.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                history_Text("sr.RecognizeAsync(RecognizeMode.Multiple); - inline <error>");
            }
            ready_for_new_command = true;
            if (show_ready_message)
            {
                ready_Message();
            }
            if(StopReadingBtn.Visible == true)
            {
                StopReadingBtn.Visible = false;
            }
        }
        
        private void system_Info()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C dxdiag";
                process.StartInfo = startInfo;
                process.Start();
                history_Text("Getting system information...");
                speak("Displaying system information.");
            }
        }

        private void TextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (text_box_first_time_opened == true)
            {
                TextBox.Text = string.Empty;
                text_box_first_time_opened = false;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (text_box1_fisrt_time_opened == true)
            {
                textBox1.Text = string.Empty;
                text_box1_fisrt_time_opened = false;
                if(input_string_method == "password_assign" || input_string_method == "password_change" || input_string_method == "check_password")
                {
                    textBox1.UseSystemPasswordChar = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1Tiker++;
            if (timer1Tiker == 5)
                timer1Tiker = 0;
            switch (shodan_state)
            {
                case "Advanced":
                    {
                        shodan_State_Advanced();
                        break;
                    }
                case "Ask":
                    {
                        shodan_State_Ask();
                        break;
                    }
                case "Basic":
                    {
                        shodan_State_Green();
                        break;
                    }
                case "Computer":
                    {
                        shodan_State_Orange();
                        break;
                    }
                case "Keylogger":
                    {
                        shodan_State_Keylogger();
                        break;
                    }
                case "Security":
                    {
                        shodan_State_Cyan();
                        break;
                    }
                case "Settings":
                    {
                        shodan_State_Settings();
                        break;
                    }
                case "String":
                    {
                        shodan_State_Red();
                        break;
                    }
                case "Textbox":
                    {
                        shodan_State_Yellow();
                        break;
                    }
                default:
                    {
                        shodan_State_Green();
                        break;
                    }
            }
        }

        public void turn_OFF_Computer()
        {
            if(ready_for_new_command)
            {
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C shutdown -s -t 30 -c \"Shutdown by Shodan\"";
                process.StartInfo = startInfo;
                process.Start();
                history_Text("Shuting down machine...");
                speak("Shuting down machine.");
                exit_Application();
            }
        }

        public void turn_OFF_Monitor()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = true;
                history_Text("Turning off monitor...");
                SetMonitorState(MonitorState.OFF);
                speak("Monitor has been turned off.");
            }
        }

        public void turn_ON_Monitor()
        {
            if (ready_for_new_command)
            {
                ready_for_new_command = false;
                history_Text("Turning on monitor...");
                SetMonitorState(MonitorState.ON);
                simulate_KeyPressing("ESC");
                speak("Monitor has been turned on.");
            }
        }

        
        private void write_XMLData(string element, string value)
        {
            try
            {
                string filePath = @"Shodan\Database\database.xml";
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(filePath);
                XmlNode node = xDoc.SelectSingleNode("Shodan/Data/" + element);
                if (node != null)
                {
                    node.InnerText = value;
                }
                xDoc.Save(filePath);
                xDoc = null;
            }
            catch (Exception)
            {
                /*
                 * Possible Exceptions:
                 *  System.ArgumentException
                 *  System.ArgumentNullException
                 *  System.InvalidOperationException
                 *  System.IO.DirectoryNotFoundException
                 *  System.IO.FileNotFoundException
                 *  System.IO.IOException
                 *  System.IO.PathTooLongException
                 *  System.NotSupportedException
                 *  System.Security.SecurityException
                 *  System.UnauthorizedAccessException
                 *  System.UriFormatException
                 *  System.Xml.XmlException
                 *  System.Xml.XPath.XPathException
                */
            }
        }

        public async void wait_Seconds(int n)
        {
            /*FUNCTION PROTOTYPE*/
            n *= 1000;
           /* historyPannel.Text = "Test";*/
            await Task.Delay(n);
            /*historyPannel.Text = "Succeed";*/
        }
        /*FUNCTIONS*/

        /*CLASSES*/
        /*POPUP CONTROL*/
        public class PopupWindow : System.Windows.Forms.ToolStripDropDown
        {
            private System.Windows.Forms.Control _content;
            private System.Windows.Forms.ToolStripControlHost _host;

            public PopupWindow(System.Windows.Forms.Control content)
            {
                //Basic setup...
                this.AutoSize = false;
                this.DoubleBuffered = true;
                this.ResizeRedraw = false;

                this._content = content;
                this._host = new System.Windows.Forms.ToolStripControlHost(content);

                //Positioning and Sizing
                this.MinimumSize = content.MinimumSize;
                this.MaximumSize = content.Size;
                this.Size = content.Size;
                content.Location = Point.Empty;

                //Add the host to the list
                this.Items.Add(this._host);
            }
        }
        /*POPUP CONTROL*/

        /*CD TRAY*/
        public class CDTray
        {
            [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
            public static extern void mciSendStringA(string lpstrCommand, string lpstrReturnString, Int32 uReturnLength, Int32 hwndCallback);
            public static void SendCDCommand(string command)
            {
                string rt = "";
                mciSendStringA(command, rt, 127, 0);
            }
        }
        /*CD TRAY*/

        /*MONITOR CONTROL*/
        private int SC_MONITORPOWER = 0xF170;
        private uint WM_SYSCOMMAND = 0x0112;
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        private void SetMonitorState(MonitorState state)
        {
            Form frm = new Form();
            SendMessage(frm.Handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)state);
        }
        /*FUNCTIONS: SetMonitorState(MonitorState.ON); SetMonitorState(MonitorState.OFF);*/
        /*MONITOR CONTROL*/
        /*CLASSES*/

        /*REMOTE CONTROL*/
        private void ShowCPbtn_Click(object sender, EventArgs e)
        {
            show_CommandsPannel();
        }

        private void HideCPbtn_Click(object sender, EventArgs e)
        {
            hide_CommandsPannel();
        }

        private void RemoteBtn_Click(object sender, EventArgs e)
        {
            if(buttons_visible == true)
            {
                buttons_visible = false;
                RemoteBtn.BackColor = System.Drawing.Color.Maroon;
                all_RemoteButtonsInvisible();
            }
            else
            {
                buttons_visible = true;
                RemoteBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
                buttons_LoadRemoteButtons();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch(shodan_state)
            {
                case "Advanced":
                    {
                        show_Networks();
                        break;
                    }
                case "Ask":
                    {
                        answer_YES_Command();
                        break;
                    }
                case "Basic":
                    {
                        advanced_Commands();
                        break;
                    }
                case "Computer":
                    {
                        ask_for_turn_OFF_Computer();
                        break;
                    }
                case "Keylogger":
                    {
                        keylogger_StartRecording();
                        break;
                    }
                case "Settings":
                    {
                        change_Username();
                        break;
                    }
                case "Textbox":
                    {
                        read_Everything();
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Advanced":
                    {
                        scan_BluetoothDevices();
                        break;
                    }
                case "Ask":
                    {
                        answer_NO_Command();
                        break;
                    }
                case "Basic":
                    {
                        security_Commands();
                        break;
                    }
                case "Computer":
                    {
                        ask_for_restart_Computer();
                        break;
                    }
                case "Keylogger":
                    {
                        keylogger_StopRecording();
                        break;
                    }
                case "Settings":
                    {
                        change_Password();
                        break;
                    }
                case "Textbox":
                    {
                        read_Selected();
                        break;
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Advanced":
                    {
                        check_Password("keylogger");
                        break;
                    }
                case "Basic":
                    {
                        show_Textbox();
                        break;
                    }
                case "Computer":
                    {
                        system_Info();
                        break;

                    }
                case "Keylogger":
                    {
                        show_KeyLog();
                        break;

                    }
                case "Settings":
                    {
                        check_Password("show_log");
                        break;
                    }
                case "Textbox":
                    {
                        clear_Content();
                        break;
                    }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Advanced":
                    {
                        mobile_Shodan();
                        break;
                    }
                case "Basic":
                    {
                        computer_Commands();
                        break;
                    }
                case "Keylogger":
                    {
                        ask_for_keylogger_clearLog();
                        break;
                    }
                case "Settings":
                    {
                        switch_Reply();
                        break;
                    }
                case "Textbox":
                    {
                        clear_Selected();
                        break;
                    }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Advanced":
                    {
                        mobile_Shodan_disable();
                        break;
                    }
                case "Basic":
                    {
                        open_CD_Tray();
                        break;
                    }
                case "Textbox":
                    {
                        ipx_CryptText();
                        break;
                    }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Basic":
                    {
                        say_Time();
                        break;
                    }
                case "Textbox":
                    {
                        ipx_DecryptText();
                        break;
                    }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Basic":
                    {
                        settings_Commands();
                        break;
                    }
            }
            }

        private void button8_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Basic":
                    {
                        minimize_Shodan();
                        break;
                    }
                case "Textbox":
                    {
                        ipx_CryptText();
                        break;
                    }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            switch (shodan_state)
            {
                case "Basic":
                    {
                        hide_Shodan();
                        break;
                    }
                case "Textbox":
                    {
                        ipx_DecryptText();
                        break;
                    }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            switch (shodan_state)
            {
                case "Basic":
                    {
                        exit_Application();
                        break;
                    }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void ListenBtn_Click(object sender, EventArgs e)
        {
            if(listening == true)
            {
                stop_Listening();
            }
            else
            {
                start_Listening();
            }
        }

        private void Shodan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(notifyIcon.Visible == true)
            {
                notifyIcon.Visible = false;
            }
            if(forcedclosed)
            {
                history_Text("Application closed by force.");
            }
        }

        private void StopReadingBtn_Click(object sender, EventArgs e)
        {
            stop_Reading();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            show_Shodan();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            show_Shodan();
        }
        
        /*BALOON TIPS*/
        private void ShowCPbtn_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 0;
            display_RemoteButtonInfo();
        }

        private void ShowCPbtn_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void HideCPbtn_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = -1;
            display_RemoteButtonInfo();
        }

        private void HideCPbtn_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 1;
            display_RemoteButtonInfo();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 2;
            display_RemoteButtonInfo();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 3;
            display_RemoteButtonInfo();
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 4;
            display_RemoteButtonInfo();
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 5;
            display_RemoteButtonInfo();
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 6;
            display_RemoteButtonInfo();
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 7;
            display_RemoteButtonInfo();
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            button_mouse_hover = 8;
            display_RemoteButtonInfo();
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            display_Remote_Shodan_info();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            label1.ForeColor = System.Drawing.Color.SpringGreen;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            display_Keylogger_info();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
            label1.ForeColor = System.Drawing.Color.SpringGreen;
        }

        private void ListenBtn_MouseHover(object sender, EventArgs e)
        {
            if (listening)
            {
                button_mouse_hover = 51;
            }
            else
            {
                button_mouse_hover = 50;
            }
            display_RemoteButtonInfo();
        }

        private void ListenBtn_MouseLeave(object sender, EventArgs e)
        {
            hide_RemoteButtonInfo();
        }
    }
}
