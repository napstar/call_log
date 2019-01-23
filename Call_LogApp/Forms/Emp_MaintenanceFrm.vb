Public Class Emp_MaintenanceFrm
    Public Sub Emp_MaintenanceFrm()

    End Sub
    Public Sub Emp_MaintenanceFrm(strPhoneNUm As String)

    End Sub
    Public Sub ReceiveValue(ByVal value As String)
        TextBox1.Text = value
        Me.DialogResult = DialogResult.OK
        Me.Activate()
        Me.Refresh()
    End Sub



    Function GetAllEmployees() As List(Of EmployeeViewModel)
        Dim empVM As New EmployeeViewModel
        Dim list As List(Of EmployeeViewModel) = empVM.SelectAll()
        Console.WriteLine(list.Count)
        Return list
    End Function

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        If ComboBox1.SelectedIndex > 0 Then
            Dim empVM As New EmployeeViewModel
            Dim empID As Integer
            empID = ComboBox1.SelectedItem.ID1
            empVM = empVM.GetByID(empID)
            lblEmpID.Text = empVM.Id1.ToString
            txtExtension.Text = empVM.Extension1
            txtName.Text = empVM.Name1

            btnSave.Text = "Update"
        Else
            clearForm()

        End If

    End Sub

    Sub clearForm()
        lblEmpID.Text = "Employee ID"
        txtExtension.Text = ""
        txtName.Text = ""
        btnSave.Text = "Save"
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        clearForm()
    End Sub
    Private Function validateEntry() As Boolean
        Dim strEmployeeName, strEmpExtension, strValdation As String
        Dim validated As Boolean
        validated = True
        Try

            strEmpExtension = txtExtension.Text
            strEmployeeName = txtName.Text
            strValdation = "Validation error".ToUpper()

            'If ComboBox1.SelectedIndex < 1 Then
            '    Throw New Exception("Please Select a Valid Employee")
            '    validated = False
            'End If
            If strEmployeeName.Length > 0 Then
                validated = True

            Else
                Throw New Exception("Please Enter the employee's name")
                validated = False
            End If
            If strEmpExtension.Length > 0 Then
                validated = True
                If strEmpExtension.Length > 5 Then
                    Throw New Exception("Employee's extension has to be 5 characters or less")
                End If

            Else
                Throw New Exception("Please Enter the employee's extension")

            End If
            'is extention unique
            Dim empVM As New EmployeeViewModel
            If empVM.GetExtensionCount(strEmpExtension) > 0 Then
                Throw New Exception("The extension " & strEmpExtension & " already exists")

            End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Validation Error")
            validated = False
        Finally

        End Try

        Return validated
    End Function
    Sub loadFormDataFomDB()
        Try
            Dim listEmployees As New List(Of EmployeeViewModel)
            listEmployees = GetAllEmployees()
            'load cmb 
            'ComboBox1.DataSource = listEmployees
            ComboBox1.Items.Clear()

            ComboBox1.Items.Add("---Select ----")
            For Each item In listEmployees
                ComboBox1.Items.Add(item)
            Next
            ComboBox1.DisplayMember = "Name1"
            ComboBox1.ValueMember = "Id1"
        Catch ex As Exception
            MsgBox("Error Loading Data FromDB.Please try again", MsgBoxStyle.OkOnly, "Save")
        End Try




    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strEmployeeName, strEmpExtension, strEmpID As String

        Try
            strEmpExtension = txtExtension.Text
            strEmployeeName = txtName.Text
            strEmpID = lblEmpID.Text

            If ComboBox1.SelectedIndex >= 1 Then
                'update
                Dim retVal As Integer
                If validateEntry() Then
                    Select Case MsgBox("Are you sure you want to Update this record?", MsgBoxStyle.YesNo, "Update")
                        Case MsgBoxResult.Yes

                            If String.IsNullOrEmpty(strEmpID) Then
                                Throw New Exception("Invalid Employee ID")
                            Else
                                Dim empVM = New EmployeeViewModel
                                empVM.Name1 = strEmployeeName
                                empVM.Extension1 = strEmpExtension
                                Try
                                    Dim t As Integer
                                    Integer.TryParse(strEmpID, t)
                                    empVM.Id1 = t
                                Catch ex As Exception
                                    Throw ex
                                End Try
                                retVal = empVM.Update(empVM)

                                If retVal > 0 Then
                                    MsgBox("The Employee " + empVM.Name1 + " has been Updated ", MsgBoxStyle.OkOnly, "Update")
                                    'reload
                                    loadFormDataFomDB()
                                    'set cmb to new user
                                    For Each comboItem In ComboBox1.Items
                                        Try
                                            Dim t As Integer
                                            If Integer.TryParse(comboItem.ID1, t) Then
                                                Dim strID As String = CStr(comboItem.ID1)
                                                If strID = CStr(empVM.Id1) Then
                                                    ComboBox1.SelectedItem = comboItem
                                                    lblEmpID.Text = strID
                                                    Exit For
                                                End If
                                            Else
                                                'not int
                                            End If

                                        Catch ex As Exception
                                        Finally

                                        End Try

                                    Next comboItem
                                Else
                                    MsgBox("Record was not updated.Please try again.If Error presists call the IT dept", MsgBoxStyle.OkOnly, "update")
                                End If

                            End If
                        Case MsgBoxResult.No
                    End Select

                End If
            Else
                'save
                If validateEntry() Then
                    Dim retVal As Integer
                    Select Case MsgBox("This action will create a new employee record.Are you sure you want to save?", MsgBoxStyle.YesNo, "Save")
                        Case MsgBoxResult.Yes
                            Dim empVM = New EmployeeViewModel
                            empVM.Name1 = strEmployeeName
                            empVM.Extension1 = strEmpExtension

                            'save 
                            retVal = empVM.Insert(empVM)
                                If (retVal > 0) Then
                                    'inform user
                                    MsgBox("The Employee " + empVM.Name1 + " has been created ", MsgBoxStyle.OkOnly, "Save")
                                    'reload
                                    loadFormDataFomDB()
                                    'set cmb to new user
                                    For Each comboItem In ComboBox1.Items
                                        Try
                                            Dim t As Integer
                                            If Integer.TryParse(comboItem.ID1, t) Then
                                                Dim strID As String = CStr(comboItem.ID1)
                                                If strID = CStr(retVal) Then
                                                    ComboBox1.SelectedItem = comboItem
                                                    lblEmpID.Text = strID
                                                    Exit For
                                                End If
                                            Else
                                                'not int
                                            End If

                                        Catch ex As Exception
                                        Finally

                                        End Try

                                    Next comboItem

                                Else
                                    MsgBox("Record was not created.Please try again.If Error presists call          the IT dept", MsgBoxStyle.OkOnly, "Save")
                                End If



                                Case MsgBoxResult.No
                            'Do nothing
                            'MessageBox.Show("NO button")
                    End Select
                End If
            End If

        Catch ex As Exception
            MsgBox("Error " + ex.Message, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try





    End Sub

    Private Sub Emp_MaintenanceFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadFormDataFomDB()
        clearForm()

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'is auser selected? 
        Dim strEmpID As String
        Try
            strEmpID = lblEmpID.Text
            If (String.IsNullOrEmpty(strEmpID) Or strEmpID = "Employee ID") Then
                MsgBox("Please Select a valid user?", MsgBoxStyle.OkOnly, "Error")
            Else
                Select Case MsgBox("Are you sure you want to Delete?", MsgBoxStyle.YesNo, "Delete")
                    Case MsgBoxResult.Yes
                        'get employee id
                        Dim empVM As New EmployeeViewModel
                        Try
                            Dim id As Integer
                            Integer.TryParse(strEmpID, id)
                            Dim retVal As Integer = empVM.Delete(id)
                            If retVal > 0 Then
                                MsgBox("Record was deleted", MsgBoxStyle.OkOnly, "Delete")

                                clearForm()
                                loadFormDataFomDB()

                            Else
                                MsgBox("Record was not deleted.Please try again.If Error presists call the IT dept", MsgBoxStyle.OkOnly, "Delete")
                            End If
                        Catch ex As Exception
                            Throw New Exception("Invalid Employee ID")
                        End Try

                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        End Try



    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Select Case MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Exit")
            Case MsgBoxResult.Yes
                Me.Close()
        End Select




    End Sub
End Class