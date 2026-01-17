# Multi-Station Control - Industrial Monitoring System

Multi-station industrial control and monitoring application using National Instruments Network Variables.
This application provides a comprehensive control and monitoring interface for industrial automation systems:

### Station Monitoring
- **Monitors 4 industrial stations simultaneously** with real-time status updates
- **Displays 16 digital inputs per station** (64 inputs total) via LED indicators
- **Reads input states** from PLC memory addresses E4.0-E4.7 and E5.0-E5.7
- **Visual feedback** with green LEDs showing active/inactive states

### Output Control
- **Controls 16 digital outputs** for Station 1 via toggle switches
- **Writes to PLC outputs** at addresses A8.0-A8.7 and A9.0-A9.7
- **Manual control mode** allowing individual output activation/deactivation
- **8 action switches** for paired output control (useful for double-acting cylinders)

### Station 1 Automation
- **Automatic cycle execution** triggered by sensor input (E4.4)
- **Sequential operations** controlling pneumatic actuators (Z1-Z8)
- **Status monitoring** showing "Busy" (orange) or "Free" (green) states
- **Event-driven logic** responding to sensor triggers at specific memory addresses

### Network Communication
- **OPC communication** with National Instruments Variable Engine
- **Network variable synchronization** at 500ms intervals
- **Support for both local and remote servers** (localhost or network addresses)
- **Real-time data exchange** between application and PLC

### User Interface
- **Intuitive graphical layout** with station organization
- **Color-coded indicators** for quick status recognition
- **Connection management** with connect/disconnect buttons
- **Error handling** with timeout detection and reconnection attempts

## Features

- Simultaneous control of 4 stations
- Real-time input/output display
- Station 1 automation
- Graphical interface with LEDs and switches
- OPC communication
- Timeout handling and error recovery
- Support for simulation and production environments

## Requirements

- Visual Studio 2019 or higher
- .NET Framework 4.7.2+
- NI Network Variable (LabVIEW Runtime)
- National Instruments DAQmx

## Installation

1. Clone the repository
2. Open the .sln file in Visual Studio
3. Build and run

## Configuration

Modify network paths in `Form1.vb`:
- **Production server**: `\\196.203.130.84\cellule\OPC\automate_simul_8bits\station_1\...`
- **Local simulation**: `\\localhost\cellule\entress_4_5` (requires NI Distributed System Manager)

### Network Variable Setup
Each station requires two UInt16 network variables:
- **Inputs**: `entress_4_5` (or `entrees_4_5_sX` for stations 2-4)
- **Outputs**: `sorties_8_9` (or `sorties_8_9_sX` for stations 2-4)

## Usage

1. **Connect**: Click "connected" to establish connection with the OPC server
2. **Monitor inputs**: Observe LED indicators for all 4 stations (64 inputs total)
3. **Control outputs**: Toggle switches to manually control Station 1 outputs
4. **Use action switches**: Use paired control for actuators (Z1-Z8)
5. **Trigger automation**: Activate input E4.4 (value 16) to start Station 1 automated cycle
6. **Monitor status**: Check connection status and automation state in the status label
7. **Disconnect**: Click "disconnected" to safely close the connection

### Automation Sequence (Station 1)
The automated cycle follows this sequence:
1. Sensor E4.4 triggers cycle start
2. Activates actuators Z1 and Z2
3. Responds to sensors E4.0, E4.1, E4.5, E4.8, E5.6
4. Sequentially controls actuators Z3-Z8
5. Returns to idle state when complete

## Testing with Simulation

1. Open **NI Distributed System Manager**
2. Create variables under `localhost\cellule\`
3. Set input values manually (e.g., 255 for all inputs on)
4. Run the application and connect to localhost
5. Observe LED responses to variable changes

## Technical Details

- **Language**: Visual Basic .NET
- **Framework**: Windows Forms
- **Communication**: National Instruments Network Variables (NI-PSP)
- **Data type**: UInt16 (16-bit unsigned integers)
- **Update rate**: 500ms timer interval
- **Timeout**: 5000ms for network reads
