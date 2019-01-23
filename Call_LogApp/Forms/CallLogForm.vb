Imports Call_LogApp

Public Class CallLogForm

    Private Sub CreateNewCompany(strPhoneNumber As String)
        Try

            Dim frm2 As New ForeignCompanyFrm
            'Dim strCompanyID As String = frm2.GetSelection(TextBox1.Text)
            Dim strCompanyID As String = frm2.GetSelection(strPhoneNumber)
            'MsgBox(strCompanyID, MsgBoxStyle.OkOnly)
            'validate
            If Not String.IsNullOrEmpty(strCompanyID) Then
                '
                If strCompanyID.ToUpper().Equals("Company ID".ToUpper) Then
                    'company was not created
                    Throw New Exception("Company was not created.Please try again")
                Else
                    Dim newCompnayID As Integer = 0
                    Try
                        newCompnayID = CInt(strCompanyID)
                        If newCompnayID > 0 Then
                            'set combo box to company
                            LoadFromDB()
                            If cmbForeignCompany.Items.Count > 0 Then

                                For Each comboItem In cmbForeignCompany.Items
                                    Try
                                        Dim t As Integer
                                        If Integer.TryParse(comboItem.ID1, t) Then
                                            Dim strID As String = CStr(comboItem.ID1)
                                            If strID = CStr(newCompnayID) Then
                                                cmbForeignCompany.SelectedItem = comboItem

                                                Exit For
                                            End If
                                        Else
                                            'not an int
                                        End If

                                    Catch ex As Exception
                                    Finally

                                    End Try

                                Next comboItem
                            End If


                        Else

                            Throw New Exception("Invalid CompanyID:Company was not created.Please try again")
                        End If
                    Catch ex As Exception
                        Throw New Exception("Invalid CompanyID:Company was not created.Please try again")
                    End Try


                End If
            Else
                'company was not created
                Throw New Exception("Company was not created.Please try again")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkCancel, "Error")
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



    End Sub
    Private Sub GetCompanyByPhone(strPhone As String)
        Dim companyViewModel As New ForeignCompanyViewModel
        Try
            If strPhone.Length > 0 Then
                companyViewModel = companyViewModel.GetCompanyByPhoneNum(strPhone)
                If companyViewModel.Id1 > 0 Then
                    'populate form
                    For Each comboItem In cmbForeignCompany.Items
                        Try
                            Dim t As Integer
                            If Integer.TryParse(comboItem.ID1, t) Then
                                Dim strID As String = CStr(comboItem.ID1)
                                If strID = CStr(companyViewModel.Id1) Then
                                    cmbForeignCompany.SelectedItem = comboItem

                                    Exit For
                                End If
                            End If
                        Catch ex As Exception
                        Finally

                        End Try
                    Next
                    'populateForm(companyViewModel)
                Else
                    'ask user if they wan to create a new form
                    Select Case MsgBox("There is no company attached to this phone number.Do you want to create it?", MsgBoxStyle.YesNo, "Create New Foreign Company?")
                        Case MsgBoxResult.Yes
                            CreateNewCompany(strPhone)
                        Case MsgBoxResult.No

                    End Select
                End If
            Else
                Throw New Exception("Please Enter a phone number")

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub



    Private Sub populateForm(callLog As CallsViewModel)

        Try
            'set call
            ' cmbExistingCall.Items.Clear()

            For Each comboItem In cmbExistingCall.Items
                Try
                    Dim t As Integer
                    If Integer.TryParse(comboItem.ID1, t) Then
                        Dim strID As String = CStr(comboItem.ID1)
                        If strID = CStr(callLog.Id1) Then
                            cmbExistingCall.SelectedItem = comboItem

                            Exit For
                        End If
                    End If
                Catch ex As Exception
                Finally

                End Try
            Next
            'set comapany
            ' cmbForeignCompany.Items.Clear()

            For Each comboItem In cmbForeignCompany.Items
                Try
                    Dim t As Integer
                    If Integer.TryParse(comboItem.ID1, t) Then
                        Dim strID As String = CStr(comboItem.ID1)
                        If strID = CStr(callLog.CompanyID1) Then
                            cmbForeignCompany.SelectedItem = comboItem

                            Exit For
                        End If
                    End If
                Catch ex As Exception
                Finally

                End Try
            Next
            'set employee
            'cmbEmployee.Items.Clear()
            For Each comboItem In cmbEmployee.Items
                Try
                    Dim t As Integer
                    If Integer.TryParse(comboItem.ID1, t) Then
                        Dim strID As String = CStr(comboItem.ID1)
                        If strID = CStr(callLog.EmployeeID1) Then
                            cmbEmployee.SelectedItem = comboItem

                            Exit For
                        End If
                    End If
                Catch ex As Exception
                Finally

                End Try
            Next
            'set phone num
            txtPhoneNum.Text = callLog.PhoneNum1
            'set date
            DateTimePicker1.Text = callLog.Date.ToString
            'set duration
            txtDuration.Text = CStr(callLog.Duration1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try
    End Sub

    Private Sub LoadFromDB()

        Try
            Dim listCompanies As New List(Of ForeignCompanyViewModel)
            Dim listEmployees As New List(Of EmployeeViewModel)
            Dim listCalls As New List(Of CallsViewModel)

            cmbForeignCompany.Items.Clear()
            cmbEmployee.Items.Clear()
            cmbExistingCall.Items.Clear()




            listCompanies = GetAllCompaines()
            'load cmb company 
            cmbForeignCompany.Items.Add("---Select ----")
            For Each item In listCompanies
                cmbForeignCompany.Items.Add(item)
            Next
            'cmbForeignCompany.DataSource = listCompanies
            cmbForeignCompany.DisplayMember = "Name1"
            cmbForeignCompany.ValueMember = "Id1"

            'load employees
            listEmployees = GetAllEmployees()
            cmbEmployee.Items.Add("---Select ----")
            For Each item In listEmployees
                cmbEmployee.Items.Add(item)
            Next
            'cmbEmployee.DataSource = listEmployees
            cmbEmployee.DisplayMember = "Name1"
            cmbEmployee.ValueMember = "Id1"

            listCalls = GetAllCalls()
            cmbExistingCall.Items.Add("---Select ----")
            For Each item In listCalls
                cmbExistingCall.Items.Add(item)
            Next
            cmbExistingCall.DisplayMember = "EmployeeName1"
            cmbExistingCall.ValueMember = "Id1"
        Catch ex As Exception
            MsgBox("Error Loading Data FromDB.Please try again", MsgBoxStyle.OkOnly, "Save")
        End Try

    End Sub

    Private Function GetAllCompaines() As List(Of ForeignCompanyViewModel)
        Dim viewModel As New ForeignCompanyViewModel
        Dim retVal As New List(Of ForeignCompanyViewModel)
        retVal = viewModel.SelectAll()


        Return retVal
    End Function
    Private Function GetAllEmployees() As List(Of EmployeeViewModel)
        Dim viewModel As New EmployeeViewModel
        Dim retVal As New List(Of EmployeeViewModel)
        retVal = viewModel.SelectAll()


        Return retVal
    End Function
    Private Function GetAllCalls() As List(Of CallsViewModel)
        Try
            Dim viewModel As New CallsViewModel
            Dim retVal As New List(Of CallsViewModel)
            retVal = viewModel.SelectAll()


            Return retVal
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function GetCallByID(callID As Integer) As CallsViewModel
        Dim retVal As New CallsViewModel
        Try
            If callID > 0 Then
                retVal = retVal.GetByID(callID)

            Else
                Throw New Exception("Invalid Call ID,Call ID must be greater than zero")
            End If
        Catch ex As Exception
            Throw ex

        End Try
        Return retVal

    End Function
    Private Sub DoWhatYouWantWithThisValue(text As String)
        MsgBox(text, MsgBoxStyle.OkCancel)
    End Sub

    Private Sub txtPhoneNum_Leave(sender As Object, e As EventArgs) Handles txtPhoneNum.Leave
        GetCompanyByPhone(txtPhoneNum.Text)

        ' CreateNewCompany(txtPhoneNum.Text)

        'populate form with 
    End Sub

    Private Sub CallLogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFromDB()
        clearFrom()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If validateCall() Then
                Dim callLogViewModel As New CallsViewModel
                Dim id, duration, employeeID, companyID, retVal As Integer
                Dim callDate As Date
                'update

                If cmbExistingCall.SelectedIndex > 0 Then
                    Select Case MsgBox("Are You sure you want to Update this record?", MsgBoxStyle.YesNo, "Update")
                        Case MsgBoxResult.Yes
                            id = CInt(cmbExistingCall.SelectedItem.ID1)
                            employeeID = CInt(cmbEmployee.SelectedItem.ID1)
                            duration = CInt(txtDuration.Text)
                            callDate = CDate(DateTimePicker1.Text)

                            callLogViewModel.Id1 = id
                            callLogViewModel.PhoneNum1 = txtPhoneNum.Text
                            callLogViewModel.Date = callDate
                            callLogViewModel.Duration1 = duration
                            callLogViewModel.EmployeeID1 = employeeID
                            callLogViewModel.EmployeeName1 = cmbEmployee.SelectedItem.Name1
                            companyID = CInt(cmbForeignCompany.SelectedItem.ID1)
                            callLogViewModel.CompanyID1 = companyID
                            callLogViewModel.CompanyName1 = cmbForeignCompany.SelectedItem.Name1
                            'Insert
                            retVal = callLogViewModel.Update(callLogViewModel)
                            If retVal > 0 Then
                                LoadFromDB()

                                populateForm(callLogViewModel.GetByID(callLogViewModel.Id1))
                                MsgBox("Call Log was Updated", MsgBoxStyle.OkOnly, "Record Updated")
                            Else

                                Throw New Exception("Error:Call Log was not Updated".ToUpper())
                            End If
                        Case MsgBoxResult.No

                    End Select

                Else
                    'save
                    Select Case MsgBox("This action will create a new Record.Continue?", MsgBoxStyle.YesNo, "Save")
                        Case MsgBoxResult.Yes
                            id = 0
                            employeeID = CInt(cmbEmployee.SelectedItem.ID1)
                            duration = CInt(txtDuration.Text)
                            callDate = CDate(DateTimePicker1.Text)

                            'callLogViewModel.Id1 = id
                            callLogViewModel.PhoneNum1 = txtPhoneNum.Text
                            callLogViewModel.Date = callDate
                            callLogViewModel.Duration1 = duration
                            callLogViewModel.EmployeeID1 = employeeID
                            callLogViewModel.EmployeeName1 = cmbEmployee.SelectedItem.Name1
                            companyID = CInt(cmbForeignCompany.SelectedItem.ID1)
                            callLogViewModel.CompanyID1 = companyID
                            callLogViewModel.CompanyName1 = cmbForeignCompany.SelectedItem.Name1
                            'Insert
                            retVal = callLogViewModel.Insert(callLogViewModel)
                            If retVal > 0 Then
                                LoadFromDB()
                                populateForm(callLogViewModel.GetByID(retVal))
                                MsgBox("Call Log was created", MsgBoxStyle.OkOnly, "Record Created")
                            Else

                                Throw New Exception("Error:Call Log was not Created".ToUpper())
                            End If
                        Case MsgBoxResult.No

                    End Select

                End If
                ' 

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error".ToUpper())
        Finally

        End Try
    End Sub

    Private Function validateCall() As Boolean
        Dim validated As Boolean
        Dim duration As Integer
        validated = True
        Try
            'If cmbExistingCall.SelectedIndex < 1 Then
            '    Throw New Exception("Select an existing call")
            'End If
            If String.IsNullOrEmpty(txtPhoneNum.Text) Then
                Throw New Exception("Please enter a phone")

            End If
            If cmbForeignCompany.SelectedIndex < 1 Then
                Throw New Exception("Select an existing company")
            End If
            If String.IsNullOrEmpty(txtDuration.Text) Then
                Throw New Exception("Please a enter a valid duration")
            Else
                Try
                    Dim callDuration As Integer
                    If Integer.TryParse(txtDuration.Text, callDuration) Then
                        If callDuration <= 0 Then
                            Throw New Exception("Duration must be greater tha zero")
                        End If
                    Else
                        Throw New Exception("Duration must be a valid number")
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If
            If cmbEmployee.SelectedIndex < 1 Then
                Throw New Exception("Select an existing employee")
            End If
            'phone number must exist and match company
            If Not String.IsNullOrEmpty(txtPhoneNum.Text) Then
                Dim companyVM As New ForeignCompanyViewModel
                companyVM = companyVM.GetCompanyByPhoneNum(txtPhoneNum.Text)
                If companyVM.Id1 > 0 Then
                    'Dim currentCompanyID As Integer
                    'currentCompanyID = cmbForeignCompany.SelectedItem.Id1
                    If companyVM.Phonenum1 <> txtPhoneNum.Text Then
                        'create compnay first
                        Select Case MsgBox("Phone number does not match any existing company.Do you wish to create it?", MsgBoxStyle.YesNo, "Create Company")
                            Case MsgBoxResult.Yes
                                CreateNewCompany(txtPhoneNum.Text)
                            Case MsgBoxResult.No
                                Throw New Exception("Phone number does not mach any existing company.Please create it first")
                        End Select
                    End If
                Else
                    'create compnay first
                    Select Case MsgBox("Phone number does not match any existing company.Do you wish to create it?", MsgBoxStyle.YesNo, "Create Company")
                        Case MsgBoxResult.Yes
                            CreateNewCompany(txtPhoneNum.Text)
                        Case MsgBoxResult.No
                            Throw New Exception("Phone number does not mach any existing company.Please create it first")
                    End Select


                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Validation Error")
            validated = False
        End Try

        Return validated
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Select Case MsgBox("Are You sure you want to Delete This record?", MsgBoxStyle.YesNo, "Confrim Delete")
                Case MsgBoxResult.Yes
                    Dim callID, retval As Integer
                    retval = -1

                    callID = cmbExistingCall.SelectedItem.ID1
                    If callID >= 1 Then
                        Dim callViewModel As New CallsViewModel
                        retval = callViewModel.Delete(callID)
                        If retval > 0 Then
                            MsgBox("Call was Deleted", MsgBoxStyle.OkOnly)

                            clearFrom()
                            LoadFromDB()


                        Else
                            Throw New Exception("Error in deleting record")
                        End If
                    Else
                        Throw New Exception("Please select an existing call")
                    End If
                Case MsgBoxResult.No

            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        clearFrom()
    End Sub
    Sub clearFrom()
        Try
            If cmbExistingCall.Items.Count > 0 Then
                cmbExistingCall.SelectedIndex = 0
            End If
            If cmbEmployee.Items.Count > 0 Then
                cmbEmployee.SelectedIndex = 0
            End If
            If cmbForeignCompany.Items.Count > 0 Then
                cmbForeignCompany.SelectedIndex = 0
            End If
            txtPhoneNum.Text = ""
            txtDuration.Text = ""
        Catch ex As Exception
        Finally

        End Try



    End Sub

    Private Sub cmbExistingCall_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbExistingCall.SelectionChangeCommitted
        If cmbExistingCall.SelectedIndex >= 1 Then
            'get id
            Dim id As Integer
            Try
                btnSave.Text = "Update"
                id = cmbExistingCall.SelectedItem.ID1
                If id > 0 Then
                    populateForm(GetCallByID(id))
                Else
                    Throw New Exception("Invalid Comapany ID")
                End If
            Catch ex As Exception
                btnSave.Text = "Save"
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")

            Finally


            End Try
        Else
            btnSave.Text = "Save"
            clearFrom()

        End If


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Select Case MsgBox("Are You sure you want to Exit?", MsgBoxStyle.YesNo, "Exit")
            Case MsgBoxResult.Yes
                Me.Close()
            Case MsgBoxResult.No

        End Select

    End Sub

    Private Sub cmbForeignCompany_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbForeignCompany.SelectionChangeCommitted
        'get phone num of slected compnay and set
        Dim id As Integer
        id = cmbForeignCompany.SelectedItem.ID1
        Dim foreignVM As New ForeignCompanyViewModel
        foreignVM = foreignVM.GetByID(id)
        txtPhoneNum.Text = foreignVM.Phonenum1
    End Sub
End Class