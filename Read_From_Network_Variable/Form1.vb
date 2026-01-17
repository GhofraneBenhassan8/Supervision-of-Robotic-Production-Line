Imports NationalInstruments.NetworkVariable
Imports NationalInstruments.UI.WindowsForms

Public Class Form1
    Private readerEntres As NetworkVariableReader(Of UShort)
    Private writerSorties As NetworkVariableWriter(Of UShort)
    Private readerSorties As NetworkVariableReader(Of UShort)

    Private readerEntres2 As NetworkVariableReader(Of UShort)
    Private writerSorties2 As NetworkVariableWriter(Of UShort)
    Private readerSorties2 As NetworkVariableReader(Of UShort)

    Private readerEntres3 As NetworkVariableReader(Of UShort)
    Private writerSorties3 As NetworkVariableWriter(Of UShort)
    Private readerSorties3 As NetworkVariableReader(Of UShort)

    Private readerEntres4 As NetworkVariableReader(Of UShort)
    Private writerSorties4 As NetworkVariableWriter(Of UShort)
    Private readerSorties4 As NetworkVariableReader(Of UShort)

    Private currentSorties As UShort = 0
    Private currentSortiesS2 As UShort = 0
    Private currentSortiesS3 As UShort = 0
    Private currentSortiesS4 As UShort = 0

    Private isConnected As Boolean = False
    Private Status_S1 As Boolean = False

    Private Trajectoire As New List(Of Point)
    Private idx As Integer = 0

    Private cheminEntrees As String = "\\196.203.130.84\cellule\OPC\automate_simul_8bits\station_1\entress_4_5"
    Private cheminSorties As String = "\\196.203.130.84\cellule\OPC\automate_simul_8bits\station_1\sorties_8_9"

    Private cheminEntrees2 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_2\entrees_4_5"
    Private cheminSorties2 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_2\sorties_8_9"

    Private cheminEntrees3 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_3\entrees_4_5"
    Private cheminSorties3 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_3\sorties_8_9"

    Private cheminEntrees4 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_4\entrees_4_5"
    Private cheminSorties4 As String = "\\196.203.130.84\cellule\OPC\automates_ciip\station_4\sorties_8_9"

    Private switchSorties(15) As Switch
    Private ledSorties(15) As Led
    Private actionSwitches(7) As Switch

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnected.Click
        Try
            readerEntres = New NetworkVariableReader(Of UShort)(cheminEntrees)
            writerSorties = New NetworkVariableWriter(Of UShort)(cheminSorties)
            readerSorties = New NetworkVariableReader(Of UShort)(cheminSorties)

            readerEntres2 = New NetworkVariableReader(Of UShort)(cheminEntrees2)
            writerSorties2 = New NetworkVariableWriter(Of UShort)(cheminSorties2)
            readerSorties2 = New NetworkVariableReader(Of UShort)(cheminSorties2)

            readerEntres3 = New NetworkVariableReader(Of UShort)(cheminEntrees3)
            writerSorties3 = New NetworkVariableWriter(Of UShort)(cheminSorties3)
            readerSorties3 = New NetworkVariableReader(Of UShort)(cheminSorties3)

            readerEntres4 = New NetworkVariableReader(Of UShort)(cheminEntrees4)
            writerSorties4 = New NetworkVariableWriter(Of UShort)(cheminSorties4)
            readerSorties4 = New NetworkVariableReader(Of UShort)(cheminSorties4)

            readerEntres.Connect()
            writerSorties.Connect()
            readerSorties.Connect()

            readerEntres2.Connect()
            writerSorties2.Connect()
            readerSorties2.Connect()

            readerEntres3.Connect()
            writerSorties3.Connect()
            readerSorties3.Connect()

            readerEntres4.Connect()
            writerSorties4.Connect()
            readerSorties4.Connect()

            isConnected = True
            Label1.Text = "Connected"
            Label1.ForeColor = Color.Green
            Timer1.Interval = 500
            Timer1.Enabled = True

        Catch ex As Exception
            isConnected = False
            Label1.Text = "Not Connected"
            Label1.ForeColor = Color.Red
            MessageBox.Show("Erreur de connexion : " & ex.Message)
        End Try
    End Sub

    Private Sub BtnDisconnect_Click(sender As Object, e As EventArgs) Handles ButtonDisconnect.Click
        Timer1.Enabled = False

        Try
            If readerEntres IsNot Nothing Then readerEntres.Disconnect()
            If writerSorties IsNot Nothing Then writerSorties.Disconnect()
            If readerSorties IsNot Nothing Then readerSorties.Disconnect()

            If readerEntres2 IsNot Nothing Then readerEntres2.Disconnect()
            If writerSorties2 IsNot Nothing Then writerSorties2.Disconnect()
            If readerSorties2 IsNot Nothing Then readerSorties2.Disconnect()

            If readerEntres3 IsNot Nothing Then readerEntres3.Disconnect()
            If writerSorties3 IsNot Nothing Then writerSorties3.Disconnect()
            If readerSorties3 IsNot Nothing Then readerSorties3.Disconnect()

            If readerEntres4 IsNot Nothing Then readerEntres4.Disconnect()
            If writerSorties4 IsNot Nothing Then writerSorties4.Disconnect()
            If readerSorties4 IsNot Nothing Then readerSorties4.Disconnect()
        Catch
        End Try

        isConnected = False
        Label1.Text = "Not Connected"
        Label1.ForeColor = Color.Red
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not isConnected Then Exit Sub
        If readerEntres Is Nothing Then Exit Sub

        Try
            Dim data As NetworkVariableData(Of UShort) = readerEntres.ReadData(TimeSpan.FromMilliseconds(5000))
            Dim dataSorties As NetworkVariableData(Of UShort) = readerSorties.ReadData(TimeSpan.FromMilliseconds(5000))

            Dim data2 As NetworkVariableData(Of UShort) = readerEntres2.ReadData(TimeSpan.FromMilliseconds(5000))
            Dim data3 As NetworkVariableData(Of UShort) = readerEntres3.ReadData(TimeSpan.FromMilliseconds(5000))
            Dim data4 As NetworkVariableData(Of UShort) = readerEntres4.ReadData(TimeSpan.FromMilliseconds(5000))

            If Not data.HasValue Then
                TextBox1.Text = "(pas de valeur)"
                Exit Sub
            End If

            Dim valeur As UShort = data.GetValue()
            Dim valeurSorties As UShort = If(dataSorties.HasValue, dataSorties.GetValue(), 0US)
            Dim valeur2 As UShort = If(data2.HasValue, data2.GetValue(), 0US)
            Dim valeur3 As UShort = If(data3.HasValue, data3.GetValue(), 0US)
            Dim valeur4 As UShort = If(data4.HasValue, data4.GetValue(), 0US)

            TextBox1.Text = valeur.ToString()
            TextBox2.Text = valeurSorties.ToString()

            Dim leds() As Led = {
                Led1, Led2, Led3, Led4, Led5, Led6, Led7, Led8,
                Led9, Led10, Led11, Led12, Led13, Led14, Led15, Led16
            }

            Dim leds2() As Led = {
                LedS2_1, LedS2_2, LedS2_3, LedS2_4,
                LedS2_5, LedS2_6, LedS2_7, LedS2_8,
                LedS2_9, LedS2_10, LedS2_11, LedS2_12,
                LedS2_13, LedS2_14, LedS2_15, LedS2_16
            }

            Dim leds3() As Led = {
                LedS3_1, LedS3_2, LedS3_3, LedS3_4,
                LedS3_5, LedS3_6, LedS3_7, LedS3_8,
                LedS3_9, LedS3_10, LedS3_11, LedS3_12,
                LedS3_13, LedS3_14, LedS3_15, LedS3_16
            }

            Dim leds4() As Led = {
                LedS4_1, LedS4_2, LedS4_3, LedS4_4,
                LedS4_5, LedS4_6, LedS4_7, LedS4_8,
                LedS4_9, LedS4_10, LedS4_11, LedS4_12,
                LedS4_13, LedS4_14, LedS4_15, LedS4_16
            }

            For i As Integer = 0 To 15
                Dim bitActif As Boolean = (((valeur >> i) And 1US) = 1US)
                Dim bitActif2 As Boolean = (((valeur2 >> i) And 1US) = 1US)
                Dim bitActif3 As Boolean = (((valeur3 >> i) And 1US) = 1US)
                Dim bitActif4 As Boolean = (((valeur4 >> i) And 1US) = 1US)

                leds(i).Value = bitActif
                leds2(i).Value = bitActif2
                leds3(i).Value = bitActif3
                leds4(i).Value = bitActif4
            Next

            For i As Integer = 0 To 15
                Dim bitActif As Boolean = (((valeurSorties >> i) And 1US) = 1US)

                If ledSorties(i) IsNot Nothing Then
                    ledSorties(i).Value = bitActif
                End If

                If switchSorties(i) IsNot Nothing Then
                    RemoveHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                    switchSorties(i).Value = bitActif
                    AddHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                End If
            Next

            For i As Integer = 0 To 15
                Dim bitActif As Boolean = (((valeur >> i) And 1US) = 1US)

                If bitActif Then
                    If (i = 4 AndAlso Status_S1 = False) Then
                        Status_S1 = True

                        ActionPair(currentSorties, writerSorties, 0, True)
                        ActionPair(currentSorties, writerSorties, 1, True)

                        Exit For

                    ElseIf (i = 0 AndAlso Status_S1 = False) Then
                        ActionPair(currentSorties, writerSorties, 0, False)
                        Exit For

                    ElseIf (i = 1) Then
                        ActionPair(currentSorties, writerSorties, 2, False)
                        Exit For

                    ElseIf (i = 5) Then
                        ActionPair(currentSorties, writerSorties, 2, True)
                        ActionPair(currentSorties, writerSorties, 3, True)
                        ActionPair(currentSorties, writerSorties, 5, True)
                        ActionPair(currentSorties, writerSorties, 7, True)
                        Exit For

                    ElseIf (i = 8 AndAlso Status_S1 = True) Then
                        ActionPair(currentSorties, writerSorties, 6, True)
                        Status_S1 = False
                        Exit For

                    ElseIf (i = 14 AndAlso Status_S1 = False) Then
                        ActionPair(currentSorties, writerSorties, 6, False)
                        ActionPair(currentSorties, writerSorties, 4, False)
                        Exit For
                    End If
                End If
            Next

            If Status_S1 Then
                Label1.Text = "Connected - S1 Busy"
                Label1.ForeColor = Color.Orange
            Else
                Label1.Text = "Connected - S1 Free"
                Label1.ForeColor = Color.Green
            End If

        Catch ex As TimeoutException
            Label1.Text = "Timeout lecture"
            Label1.ForeColor = Color.Orange
            readerEntres.Connect()

        Catch ex As Exception
            Timer1.Enabled = False
            isConnected = False
            Label1.Text = "Not Connected"
            Label1.ForeColor = Color.Red
            MessageBox.Show("Erreur de lecture Network Variable : " & ex.Message)
        End Try
    End Sub

    Private Sub ActionPair(
        ByRef currentValue As UShort,
        writer As NetworkVariableWriter(Of UShort),
        pairIndex As Integer,
        etat As Boolean)

        Dim bitA As Integer = pairIndex * 2
        Dim bitB As Integer = bitA + 1

        Dim maskA As UShort = CUShort(1 << bitA)
        Dim maskB As UShort = CUShort(1 << bitB)

        If etat Then
            currentValue = CUShort(currentValue Or maskA)
            currentValue = CUShort(currentValue And Not maskB)
        Else
            currentValue = CUShort(currentValue And Not maskA)
            currentValue = CUShort(currentValue Or maskB)
        End If

        writer.WriteValue(currentValue)
    End Sub

    Private Sub ActionSwitch_Changed(sender As Object, e As EventArgs)
        If Not isConnected Then Exit Sub

        Dim sw As Switch = CType(sender, Switch)
        Dim pairIndex As Integer = CInt(sw.Tag)

        Try
            ActionPair(currentSorties, writerSorties, pairIndex, sw.Value)
        Catch ex As Exception
            MessageBox.Show("Erreur d'écriture : " & ex.Message)
        End Try
    End Sub

    Private Sub InitTrajectoire()
        Trajectoire.Clear()

        For x As Integer = 885 To 228 Step -3
            Trajectoire.Add(New Point(x, 254))
        Next

        For x As Integer = 228 To 898 Step 3
            Trajectoire.Add(New Point(x, 335))
        Next
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If idx >= Trajectoire.Count Then
            idx = 0
        End If

        idx += 1
    End Sub

    Private Sub BtnStartAnimation_Click(sender As Object, e As EventArgs)
        ActionPair(currentSorties, writerSorties, 0, False)
        ActionPair(currentSorties, writerSorties, 1, False)
        ActionPair(currentSorties, writerSorties, 2, True)
        ActionPair(currentSorties, writerSorties, 3, False)
        ActionPair(currentSorties, writerSorties, 5, False)
        ActionPair(currentSorties, writerSorties, 7, False)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            For i As Integer = 0 To 15
                Dim switchName As String = "Switch" & (i + 1).ToString()
                Dim foundControls = Me.Controls.Find(switchName, True)
                If foundControls.Length > 0 Then
                    switchSorties(i) = CType(foundControls(0), Switch)
                    AddHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                End If
            Next

            For i As Integer = 0 To 15
                Dim ledName As String = "Led" & (i + 17).ToString()
                Dim foundControls = Me.Controls.Find(ledName, True)
                If foundControls.Length > 0 Then
                    ledSorties(i) = CType(foundControls(0), Led)
                End If
            Next

            For i As Integer = 0 To 7
                Dim actionName As String = "action" & (i + 1).ToString()
                Dim foundControls = Me.Controls.Find(actionName, True)
                If foundControls.Length > 0 Then
                    actionSwitches(i) = CType(foundControls(0), Switch)
                    actionSwitches(i).Tag = i
                    AddHandler actionSwitches(i).StateChanged, AddressOf ActionSwitch_Changed
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Erreur initialisation: " & ex.Message)
        End Try
    End Sub

    Private Sub Switch_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs)
        Dim sw As Switch = CType(sender, Switch)

        For i As Integer = 0 To 15
            If switchSorties(i) Is sw Then
                If sw.Value Then
                    currentSorties = currentSorties Or CUShort(1US << i)
                Else
                    Dim mask As UShort = CUShort(Not (1US << i))
                    currentSorties = currentSorties And mask
                End If

                WriteSorties()
                Exit For
            End If
        Next
    End Sub

    Private Sub WriteSorties()
        If Not isConnected Then
            MessageBox.Show("Pas connecté!")
            Exit Sub
        End If
        If writerSorties Is Nothing Then Exit Sub

        Try
            writerSorties.WriteValue(currentSorties)
            If TextBox2 IsNot Nothing Then
                TextBox2.Text = currentSorties.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur écriture sorties: " & ex.Message)
        End Try
    End Sub

    Public Sub SetSortie(numSortie As Integer, valeur As Boolean)
        If numSortie < 0 Or numSortie > 15 Then Exit Sub

        If valeur Then
            currentSorties = currentSorties Or CUShort(1US << numSortie)
        Else
            Dim mask As UShort = CUShort(Not (1US << numSortie))
            currentSorties = currentSorties And mask
        End If

        If switchSorties(numSortie) IsNot Nothing Then
            RemoveHandler switchSorties(numSortie).StateChanged, AddressOf Switch_StateChanged
            switchSorties(numSortie).Value = valeur
            AddHandler switchSorties(numSortie).StateChanged, AddressOf Switch_StateChanged
        End If

        WriteSorties()
    End Sub

    Public Sub ResetAllSorties()
        currentSorties = 0US
        For i As Integer = 0 To 15
            If switchSorties(i) IsNot Nothing Then
                RemoveHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                switchSorties(i).Value = False
                AddHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
            End If
        Next
        WriteSorties()
    End Sub

    Public Sub SetAllSorties()
        currentSorties = &HFFFFUS
        For i As Integer = 0 To 15
            If switchSorties(i) IsNot Nothing Then
                RemoveHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                switchSorties(i).Value = True
                AddHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
            End If
        Next
        WriteSorties()
    End Sub

    Public Sub ApplyDefaultSorties()
        Dim defaultStates() As Boolean = {
            True, False, True, False, True, False, False, True,
            True, False, False, False, True, False, False, True
        }

        currentSorties = 0US
        For i As Integer = 0 To 15
            If defaultStates(i) Then
                currentSorties = currentSorties Or CUShort(1US << i)
            End If
            If switchSorties(i) IsNot Nothing Then
                RemoveHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
                switchSorties(i).Value = defaultStates(i)
                AddHandler switchSorties(i).StateChanged, AddressOf Switch_StateChanged
            End If
        Next
        WriteSorties()
    End Sub

    Public Function GetSortie(numSortie As Integer) As Boolean
        If numSortie < 0 Or numSortie > 15 Then Return False
        Return (((currentSorties >> numSortie) And 1US) = 1US)
    End Function

    Public Function GetSortiesValue() As UShort
        Return currentSorties
    End Function

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Led17_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led17.StateChanged

    End Sub

    Private Sub Switch1_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch1.StateChanged

    End Sub

    Private Sub Switch2_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch2.StateChanged

    End Sub

    Private Sub Led18_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led18.StateChanged

    End Sub

    Private Sub Switch9_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch9.StateChanged

    End Sub

    Private Sub Led25_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led25.StateChanged

    End Sub

    Private Sub Led26_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led26.StateChanged

    End Sub

    Private Sub Switch10_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch10.StateChanged

    End Sub

    Private Sub Switch11_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch11.StateChanged

    End Sub

    Private Sub Led27_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led27.StateChanged

    End Sub

    Private Sub Led28_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led28.StateChanged

    End Sub

    Private Sub Switch12_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch12.StateChanged

    End Sub

    Private Sub Switch13_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch13.StateChanged

    End Sub

    Private Sub Led29_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led29.StateChanged

    End Sub

    Private Sub Led30_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led30.StateChanged

    End Sub

    Private Sub Switch14_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch14.StateChanged

    End Sub

    Private Sub Switch15_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch15.StateChanged

    End Sub

    Private Sub Led31_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led31.StateChanged

    End Sub

    Private Sub Led32_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led32.StateChanged

    End Sub

    Private Sub Switch16_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch16.StateChanged

    End Sub

    Private Sub Led24_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led24.StateChanged

    End Sub

    Private Sub Switch8_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch8.StateChanged

    End Sub

    Private Sub Switch7_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch7.StateChanged

    End Sub

    Private Sub Led23_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led23.StateChanged

    End Sub

    Private Sub Led22_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led22.StateChanged

    End Sub

    Private Sub Switch6_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch6.StateChanged

    End Sub

    Private Sub Switch5_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch5.StateChanged

    End Sub

    Private Sub Led21_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led21.StateChanged

    End Sub

    Private Sub Led20_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led20.StateChanged

    End Sub

    Private Sub Switch4_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch4.StateChanged

    End Sub

    Private Sub Switch3_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Switch3.StateChanged

    End Sub

    Private Sub Led19_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles Led19.StateChanged

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub LedS4_11_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles LedS4_11.StateChanged

    End Sub

    Private Sub LedS4_12_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles LedS4_12.StateChanged

    End Sub

    Private Sub LedS3_3_StateChanged(sender As Object, e As NationalInstruments.UI.ActionEventArgs) Handles LedS3_3.StateChanged

    End Sub
End Class