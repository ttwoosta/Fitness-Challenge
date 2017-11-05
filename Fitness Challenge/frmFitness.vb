' Program Name: Fitness Challege
' Author:       Tu Tong
' Date:         November 5, 2017
' Purpose:      The Fitness Challenge program enters the weight loss from
'               team members for a fitness challenge. It displays each 
'               weight loss value. After all weight loss values have been
'               entered, it displays the average weight loss for the team.

' Turn off automatic conversion for values
Option Strict On


Public Class frmFitness

    '
    ' Summary:
    '     Indicates which value type of the input message
    Enum InputBoxResult
        Positive = 1
        Negative = -1
        Cancelled = -2
        NonNumeric = -3
    End Enum

    ' Summary:
    '     Returns a InputBoxResult value indicating input message string
    '     can be converted To a positive number.
    ' Parameters:
    '     InputMessage:
    '       Required. string input message
    ' Returns:
    '     Returns a InputBoxResult value indicating input message string 
    '     can be converted To a positive number.
    Function convertToInputBoxResultWith(InputMessage As String) As InputBoxResult

        If InputMessage = "" Then
            Return InputBoxResult.Cancelled
        End If

        If IsNumeric(InputMessage) Then

            ' Validate that the entered value is a positive number
            If Convert.ToDecimal(InputMessage) > 0 Then
                Return InputBoxResult.Positive
            End If

            Return InputBoxResult.Negative
        End If

        Return InputBoxResult.NonNumeric
    End Function

    Private Sub btnWeightLoss_Click(sender As Object, e As EventArgs) Handles btnWeightLoss.Click
        ' The btnWeightLoss_Click event accepts and displays up to 8 weight loss values,
        ' and then calculates and displays the average weight loss for the team.

        ' Declare and initialize variables
        Dim strWeightLoss As String
        Dim decWeightLoss As Decimal
        Dim decAverageLoss As Decimal
        Dim decTotalWeightLoss As Decimal = 0D

        ' Declare message box contants
        Const cstrInputHeading As String = "Weight Loss"
        Const cstrNormalMessage As String = "Enter the weight loss for team member #"
        Const cstrNonNumericError As String = "Error - Enter a number for the weight loss of team memnber #"
        Const cstrNegativeError As String = "Error - Enter a positive number for the weight loss of team member #"

        ' Declare and initialize variables that
        ' use with the Input Box function call
        Dim strInputMessage As String = "Enter the weight loss for team member #"
        Dim ipbResult As InputBoxResult

        ' Declare and initialize loop variables
        Const cintMaxNumberOfEntries As Integer = 8
        Dim intNumberOfEntries As Integer = 0

        ' This loop allows the user enter the weight loss of up to 8 team members. 
        ' The loop terminates when the user has entered 8 weight loss values 
        ' or the users taps or clicks the Cancel button or Close button
        ' in the InputBox
        Do Until intNumberOfEntries > cintMaxNumberOfEntries Or ipbResult = InputBoxResult.Cancelled

            ' Display the inputBox if the number of entries entered less than 8
            strWeightLoss = InputBox(strInputMessage & intNumberOfEntries, cstrInputHeading, " ")

            ' convert message to input result
            ipbResult = convertToInputBoxResultWith(strWeightLoss)

            Select Case ipbResult
                Case InputBoxResult.Positive
                    ' convert the value entered from a string to the decial data type
                    decWeightLoss = Convert.ToDecimal(strWeightLoss)

                    ' Perform the processing when the user enters a valid weight loss
                    lstWeightLoss.Items.Add(decWeightLoss)
                    decTotalWeightLoss += decWeightLoss
                    intNumberOfEntries += 1
                    strInputMessage = cstrNormalMessage

                    ' calculate the average weight loss
                    decAverageLoss = decTotalWeightLoss / intNumberOfEntries

                    ' displays the average weight loss
                    lblAverageLoss.Visible = True
                    lblAverageLoss.Text = "Average weight loss for the team is " &
                        decAverageLoss.ToString("F1") & " lbs"

                Case InputBoxResult.Negative
                    ' Assign an error message if the user enters
                    ' a negative weight loss
                    strInputMessage = cstrNegativeError

                Case InputBoxResult.NonNumeric
                    ' Assign an error message if the user enters 
                    ' a non - numeric weight loss
                    strInputMessage = cstrNonNumericError

            End Select

            ' Calculate and displays the average team weight loss
            If intNumberOfEntries = 0 Then
                MsgBox("No weight loss value entered")
            End If
        Loop

        ' Disable the Weight Loss button
        btnWeightLoss.Enabled = False

    End Sub

    Private Sub mnuClear_Click(sender As Object, e As EventArgs) Handles mnuClear.Click
        ' The mnuClear click event clears the ListBox object and hides
        ' the average weight loss label. 
        ' It also enables the weight loss button

        lstWeightLoss.Items.Clear()
        lblAverageLoss.Visible = False
        btnWeightLoss.Enabled = True

    End Sub

    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click
        ' The mnuExit click event closess the window and exits the application

        Close()
    End Sub
End Class
