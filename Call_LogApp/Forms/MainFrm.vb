Public Class MainFrm
    Private Sub EmployeeMaintenanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeMaintenanceToolStripMenuItem.Click
        Try
            Dim EmpFrm As Emp_MaintenanceFrm = New Emp_MaintenanceFrm
            Dim ActiveFromChild As Form = Me.ActiveMdiChild


            If (ActiveFromChild Is Nothing) Then
                ' create the form here 
                EmpFrm.Dock = DockStyle.None


                EmpFrm.TopLevel = False
                EmpFrm.Show()
                EmpFrm.Parent = Me
            Else
                'make it the active form 
                If ActiveFromChild.Name = "" Then
                    ActiveFromChild.ShowDialog()

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub ForeignCompanyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForeignCompanyToolStripMenuItem.Click
        'open fc form
        Dim foreignCompanyForm As ForeignCompanyFrm = New ForeignCompanyFrm
        Dim ActiveFromChild As Form = Me.ActiveMdiChild


        If (ActiveFromChild Is Nothing) Then
            ' create the form here 
            foreignCompanyForm.Dock = DockStyle.None


            foreignCompanyForm.TopLevel = False
            foreignCompanyForm.Show()
            foreignCompanyForm.Parent = Me
        Else
            'make it the active form 
            If ActiveFromChild.Name = "" Then
                ActiveFromChild.ShowDialog()

            End If
        End If
    End Sub

    Private Sub CallsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CallsToolStripMenuItem.Click
        'open call log form
        Dim callLogForm As CallLogForm = New CallLogForm
        Dim ActiveFromChild As Form = Me.ActiveMdiChild


        If (ActiveFromChild Is Nothing) Then
            ' create the form here 
            callLogForm.Dock = DockStyle.None


            callLogForm.TopLevel = False
            callLogForm.Show()
            callLogForm.Parent = Me
        Else
            'make it the active form 
            If ActiveFromChild.Name = "" Then
                ActiveFromChild.ShowDialog()

            End If
        End If
    End Sub

    Private Sub MainFrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Select Case MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Exit")
            Case MsgBoxResult.Yes
                e.Cancel = False
            Case MsgBoxResult.No
                e.Cancel = True
        End Select

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        'Select Case MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Exit")
        '    Case MsgBoxResult.Yes
        '        e.Cancel = False
        '    Case MsgBoxResult.No
        '        e.Cancel = True
        'End Select
    End Sub
End Class