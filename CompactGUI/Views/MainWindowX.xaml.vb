﻿Imports System.Windows.Media.Animation

Imports CompactGUI.MainViewModel

Class MainWindowX

    Sub New()

        InitializeComponent()

        Me.DataContext = ViewModel

        ViewModel.State = UIState.FreshLaunch

    End Sub

    Public Property ViewModel As New MainViewModel

    Private Async Sub SearchClicked(sender As Object, e As MouseButtonEventArgs)
        Await ViewModel.SelectFolderAsync()
    End Sub

    Private Sub UIUpdateBanner_MouseUp(sender As Object, e As RoutedEventArgs)


        Process.Start(New ProcessStartInfo("https://github.com/IridiumIO/CompactGUI/releases/") With {.UseShellExecute = True})
    End Sub

    Private Sub UIBtnOptions_Click(sender As Object, e As RoutedEventArgs) Handles uiBtnOptions.Click

        Dim settingsDialog As New CustomContentDialog With {.Content = New SettingsView, .PrimaryButtonText = "save and close"}
        settingsDialog.ShowAsync()

    End Sub

    Private Sub Window_PreviewKeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.System AndAlso e.SystemKey = Key.F4 Then
            Return
        ElseIf e.Key = Key.System Then
            e.Handled = True
        End If

    End Sub

    Private Async Sub Window_Drop(sender As Object, e As DragEventArgs)
        Dim xs As String() = e.Data.GetData(DataFormats.FileDrop, True)
        If xs.Length > 1 Then
            MessageBox.Show("You can only select one folder at a time")
            Return
        End If

        If IO.Directory.Exists(xs(0)) Then
            Await ViewModel.SelectFolderAsync(xs(0))
        End If


    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs)

        Me.Top = (SystemParameters.PrimaryScreenHeight - SettingsHandler.AppSettings.WindowHeight) / 2
        Me.Left = (SystemParameters.PrimaryScreenWidth - SettingsHandler.AppSettings.WindowWidth) / 2

        If Me.Top < 0 Then Me.Top = 0

    End Sub



End Class
