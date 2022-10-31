using Symbol.RFID3;
using System.Diagnostics;

namespace test01_fx7500
{
    public partial class Form1 : Form
    {

        string hostname = "169.254.10.1";
        RFIDReader rfid3;


        public Form1()
        {
            InitializeComponent();



        }


        // Connecting to Reader
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rfid3 = new RFIDReader(hostname, 0, 0);
                rfid3.Connect();
                Debug.WriteLine(rfid3.ReaderCapabilities.ModelName + " is connected.");
            }
            catch (Exception err)
            {
                Console.WriteLine("ó·äOÇ™î≠ê∂ÇµÇ‹ÇµÇΩ");
                Console.WriteLine(err);
            }

        }

        // Disconnecting the Reader
        private void button2_Click(object sender, EventArgs e)
        {
            rfid3.Disconnect();
            Debug.WriteLine("Disconnected.");
        }

        // Get Reader Capabilities
        private void button3_Click(object sender, EventArgs e)
        {
            // Get Reader capabilities
            Debug.WriteLine("Firmware = " + rfid3.ReaderCapabilities.FirwareVersion);

            Debug.WriteLine("ModelName = " + rfid3.ReaderCapabilities.ModelName);

            Debug.WriteLine("NumAntennaSupported={0}", rfid3.ReaderCapabilities.NumAntennaSupported);

            Debug.WriteLine("NumGPIPorts={0}", rfid3.ReaderCapabilities.NumGPIPorts);

            Debug.WriteLine("NumGPOPorts={0}", rfid3.ReaderCapabilities.NumGPOPorts);

            Debug.WriteLine("IsUTCClockSupported= {0}", rfid3.ReaderCapabilities.IsUTCClockSupported);

            Debug.WriteLine("IsPeriodicTagReportsSupported= {0}", rfid3.ReaderCapabilities.IsPeriodicTagReportsSupported);

            Debug.WriteLine("IsTagPhaseReportingSupported= {0}", rfid3.ReaderCapabilities.IsTagPhaseReportingSupported);

            Debug.WriteLine("IsBlockEraseSupported={0}", rfid3.ReaderCapabilities.IsBlockEraseSupported);

            Debug.WriteLine("IsBlockPermaLockSupported={0}", rfid3.ReaderCapabilities.IsBlockPermalockSupported);

            Debug.WriteLine("IsTagInventoryStateAwareSingulationSupported={0}", rfid3.ReaderCapabilities.IsTagInventoryStateAwareSingulationSupported);

            Debug.WriteLine("MaxNumOperationsInAccessSequence={0}", rfid3.ReaderCapabilities.MaxNumOperationsInAccessSequence);

            Debug.WriteLine("MaxNumPreFilters={0}", rfid3.ReaderCapabilities.MaxNumPreFilters);

            Debug.WriteLine("CommunicationStandard={0}", rfid3.ReaderCapabilities.CommunicationStandard);

            Debug.WriteLine("CountryCode={0}", rfid3.ReaderCapabilities.CountryCode);

            Debug.WriteLine("IsHoppingEnabled={0}", rfid3.ReaderCapabilities.IsHoppingEnabled);

        }

        // Get GPIO status / max port #
        private void button4_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("GPI length = {0}", rfid3.Config.GPI.Length);
            Debug.WriteLine("GPO length = {0}", rfid3.Config.GPO.Length);

            Debug.WriteLine("GPI 1 port status = {0}", rfid3.Config.GPI[1].PortState);
            Debug.WriteLine("GPI 2 port status = {0}", rfid3.Config.GPI[2].PortState);

            Debug.WriteLine("GPI 1 port enabled = {0}", rfid3.Config.GPI[1].IsEnabled);
            Debug.WriteLine("GPI 2 port enabled = {0}", rfid3.Config.GPI[2].IsEnabled);

            Debug.WriteLine("GPO 1 port status = {0}", rfid3.Config.GPO[1].PortState);
            Debug.WriteLine("GPO 2 port status = {0}", rfid3.Config.GPO[2].PortState);
            Debug.WriteLine("GPO 3 port status = {0}", rfid3.Config.GPO[3].PortState);


        }

        // Change GPI Enablement
        private void button5_Click(object sender, EventArgs e)
        {
            switch (rfid3.Config.GPI[1].IsEnabled)
            {
                case true:
                    rfid3.Config.GPI[1].Enable = false;
                    break;
                case false:
                    rfid3.Config.GPI[1].Enable = true;
                    break;
            }

            Debug.WriteLine("GPI 1 port enabled = {0}", rfid3.Config.GPI[1].IsEnabled);
        }


        // Change GPO Current Status
        private void button6_Click(object sender, EventArgs e)
        {
            switch (rfid3.Config.GPO[1].PortState)
            {
                case GPOs.GPO_PORT_STATE.TRUE:
                    rfid3.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    break;
                case GPOs.GPO_PORT_STATE.FALSE:
                    rfid3.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.TRUE;
                    break;
            }

            Debug.WriteLine("GPO 1 port status = {0}", rfid3.Config.GPO[1].PortState);



        }

        // Get Radio Power State Configuretaion 
        private void button7_Click(object sender, EventArgs e)
        {
            RADIO_POWER_STATE radioPowerState = rfid3.Config.RadioPowerState;
            Debug.WriteLine("Current radio power status is {0}", radioPowerState);

            // How to change state
            // rfid3.Config.RadioPowerState = RADIO_POWER_STATE.OFF;
            // rfid3.Config.RadioPowerState = RADIO_POWER_STATE.ON;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Antennas.AntennaRfConfig antRfConfig = rfid3.Config.Antennas[1].GetRfConfig();

            antRfConfig.AntennaStopTriggerConfig.StopTriggerType = ANTENNA_STOP_TRIGGER_TYPE.ANTENNA_STOP_TRIGGER_TYPE_DURATION_MILLISECS;
            antRfConfig.AntennaStopTriggerConfig.AntennaStopConditionValue = 400;

            rfid3.Config.Antennas[1].SetRfConfig(antRfConfig);

            Debug.WriteLine("AntennaStopConditionValue = {0}", antRfConfig.AntennaStopTriggerConfig.AntennaStopConditionValue);
            Debug.WriteLine("StopTriggerType = {0}", antRfConfig.AntennaStopTriggerConfig.StopTriggerType);


        }

        private void button9_Click(object sender, EventArgs e)
        {

            // Get Singulation Control for the antenna 2
            Antennas.SingulationControl singulationControl;
            singulationControl = rfid3.Config.Antennas[2].GetSingulationControl();

            Debug.WriteLine("Antenna 2 / Session = {0}", singulationControl.Session);
            Debug.WriteLine("Antenna 2 / Tag Population = {0}", singulationControl.TagPopulation);
            Debug.WriteLine("Antenna 2 / PerformStateAwareSingulationAction = {0}", singulationControl.Action.PerformStateAwareSingulationAction);
            Debug.WriteLine("Antenna 2 / SLFlag = {0}", singulationControl.Action.SLFlag);
            Debug.WriteLine("Antenna 2 / InventoryState = {0}", singulationControl.Action.InventoryState);
            Debug.WriteLine("Antenna 2 / TagTransitTime = {0}", singulationControl.TagTransitTime);


            // Set Singulation Control for the antenna 1 
            singulationControl.Session = SESSION.SESSION_S0;
            singulationControl.TagPopulation = 30;
            singulationControl.Action.PerformStateAwareSingulationAction = true;
            singulationControl.Action.SLFlag = SL_FLAG.SL_ALL;
            singulationControl.Action.InventoryState = INVENTORY_STATE.INVENTORY_STATE_A;
            rfid3.Config.Antennas[1].SetSingulationControl(singulationControl);

            Debug.WriteLine("Antenna 1 / Session = {0}", singulationControl.Session);
            Debug.WriteLine("Antenna 1 / Tag Population = {0}", singulationControl.TagPopulation);
            Debug.WriteLine("Antenna 1 / PerformStateAwareSingulationAction = {0}", singulationControl.Action.PerformStateAwareSingulationAction);
            Debug.WriteLine("Antenna 1 / SLFlag = {0}", singulationControl.Action.SLFlag);
            Debug.WriteLine("Antenna 1 / InventoryState = {0}", singulationControl.Action.InventoryState);
            Debug.WriteLine("Antenna 1 / TagTransitTime = {0}", singulationControl.TagTransitTime);


        }

        // Enable Status Event
        private void button10_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start Notifiying FX Status.");

            rfid3.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);

            // Subscribe required status notification
            rfid3.Events.NotifyGPIEvent = true;
            rfid3.Events.NotifyBufferFullWarningEvent = true;
            rfid3.Events.NotifyBufferFullEvent = true;
            rfid3.Events.NotifyAntennaEvent = true;
            rfid3.Events.NotifyInventoryStartEvent = true;
            rfid3.Events.NotifyInventoryStopEvent = true;
            rfid3.Events.NotifyAccessStartEvent = true;
            rfid3.Events.NotifyAccessStopEvent = true;
            rfid3.Events.NotifyReaderDisconnectEvent = true;
            rfid3.Events.NotifyReaderExceptionEvent = true;
            rfid3.Events.NotifyTemperatureAlarmEvent = true;


            // Status Notify handler

            void Events_StatusNotify(object sender, Events.StatusEventArgs e)

            {

                Debug.WriteLine("Notify Event = " + e.StatusEventData.StatusEventType);

            }

            // Unregistering for status notification
            // rfid3.Events.StatusNotify -= new Events.StatusNotifyHandler(Events_StatusNotify);

        }




        // Read Notify Event
        private void button11_Click(object sender, EventArgs e)
        {

            Debug.WriteLine("Start read notify events.");

            // registering for read tag data notification

            rfid3.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);

            // ReadNotify Event comes without tag data 

            rfid3.Events.AttachTagDataWithReadEvent = false;


            // Read Notify handler

            void Events_ReadNotify(object sender, Events.ReadEventArgs e)

            {
                Debug.WriteLine("Detect read notify events.");


                // fetch tags from the Dll by specifying the number of expected tags

                Symbol.RFID3.TagData[] myTags = rfid3.Actions.GetReadTags(100);

                if (myTags != null)
                {
                    for (int nIndex = 0; nIndex < myTags.Length; nIndex++)
                    {
                        if (myTags[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                           (myTags[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                                    myTags[nIndex].OpStatus ==ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))

                        {
                            Debug.WriteLine(myTags[nIndex].PC);
                            Debug.WriteLine(myTags[nIndex].TagID);
                            Debug.WriteLine(myTags[nIndex].AntennaID);
                            Debug.WriteLine(myTags[nIndex].PeakRSSI);
                            Debug.WriteLine(myTags[nIndex].AntennaPortInfo.ReceivePort);
                            Debug.WriteLine(myTags[nIndex].ChannelIndex);
                            Debug.WriteLine(myTags[nIndex].MemoryBank);
                            Debug.WriteLine(myTags[nIndex].MemoryBankData);
                            Debug.WriteLine(myTags[nIndex].OpCode);
                            Debug.WriteLine(myTags[nIndex].OpStatus);
                            Debug.WriteLine(myTags[nIndex].TagEvent);
                            Debug.WriteLine(myTags[nIndex].TagSeenCount);
                            Debug.WriteLine("------------");

                            if (myTags[nIndex].MemoryBankData != String.Empty)
                                Debug.WriteLine(myTags[nIndex].MemoryBank.ToString() +" : " + myTags[nIndex].MemoryBankData);

                        }
                    }
                }
            }



            // Unregistering for read tag data notification

            // rfid3.Events.ReadNotify -= new Events.ReadNotifyHandler(Events_ReadNotify);
        }

        // Simple inventory start
        private void button12_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start simple inventory.");
            rfid3.Actions.Inventory.Perform();
        }

        // Simple inventory stop
        private void button13_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Stop simple inventory.");
            rfid3.Actions.Inventory.Stop();
        }
    }
}