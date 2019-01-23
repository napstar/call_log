Imports Call_LogApp

Public Class ForeignCompanyFrm
    Public Function GetSelection(newCompanyID As String) As String

        'txtCompanyName.Text = newCompanyID
        txtPhoneNum.Text = newCompanyID
        '  TextBox1.Text = strPhoneNum
        Me.ShowDialog()

        'Return Me.TextBox1.Text
        Return Label1.Text
    End Function

    Function validateEntry() As Boolean
        Dim validated As Boolean
        validated = True
        Try
            'If cmbCompany.SelectedIndex < 1 Then
            '    Throw New Exception("Please Select a Valid company")

            'End If
            If String.IsNullOrEmpty(txtCompanyName.Text) Then
                Throw New Exception("Please Enter a Valid Company Name")

            End If
            If String.IsNullOrEmpty(txtPhoneNum.Text) Then
                Throw New Exception("Please Enter a Phone Number")

            End If
            If String.IsNullOrEmpty(txtCity.Text) Then
                Throw New Exception("Please Enter a City")

            End If
            If String.IsNullOrEmpty(txtCountry.Text) Then
                Throw New Exception("Please Enter a Country")

            End If
            If cmbCompanyContact.SelectedIndex < 1 Then
                Throw New Exception("Please Select an Employee")

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Validation Error")
            validated = False
        Finally

        End Try
        Return validated

    End Function
    Function updateCompany() As Integer
        Dim strCompanyName = txtCompanyName.Text
        Dim strCompanyPhoneNum = txtPhoneNum.Text
        Dim strCompanyCity = txtCity.Text
        Dim strCompanyCountry = txtCountry.Text
        Dim employeeID As Integer
        Dim companyID As Integer


        Dim retVal As Integer = -1
        Try
            Dim foreignCompanyVM As New ForeignCompanyViewModel
            foreignCompanyVM.Name1 = strCompanyName
            foreignCompanyVM.Phonenum1 = strCompanyPhoneNum
            foreignCompanyVM.City1 = strCompanyCity
            foreignCompanyVM.Country1 = strCompanyCountry
            employeeID = CInt(cmbCompanyContact.SelectedItem.ID1)
            companyID = CInt(cmbCompany.SelectedItem.ID1)
            foreignCompanyVM.EmployeeNum1 = employeeID
            foreignCompanyVM.Id1 = companyID
            'does the phone num match existing comapny phone?

            retVal = foreignCompanyVM.Update(foreignCompanyVM)


            If retVal > 0 Then
                foreignCompanyVM.Id1 = retVal
                MsgBox("Record has been updated", MsgBoxStyle.OkOnly, "Save")
                'load data from db
                loadDataFromDB()
                TextBox1.Text = foreignCompanyVM.Id1.ToString
                Label1.Text = foreignCompanyVM.Id1.ToString

                'set cmb to new user
                loadDataFromDB()
                populateForm(foreignCompanyVM)
            Else
                Throw New Exception("Error in Updating record")
            End If
        Catch ex As Exception
            Throw ex

        End Try

        Return retVal
    End Function
    Function saveCompany() As Integer
        Dim strCompanyName = txtCompanyName.Text
        Dim strCompanyPhoneNum = txtPhoneNum.Text
        Dim strCompanyCity = txtCity.Text
        Dim strCompanyCountry = txtCountry.Text
        Dim strEmployeeID As Integer

        Dim retVal As Integer = -1
        Try
            Dim foreignCompanyVM As New ForeignCompanyViewModel
            foreignCompanyVM.Name1 = strCompanyName
            foreignCompanyVM.Phonenum1 = strCompanyPhoneNum
            foreignCompanyVM.City1 = strCompanyCity
            foreignCompanyVM.Country1 = strCompanyCountry

            retVal = foreignCompanyVM.Insert(foreignCompanyVM)
            If retVal > 0 Then
                MsgBox("Record was created", MsgBoxStyle.OkOnly, "Save")
                foreignCompanyVM.Id1 = retVal
                'load data from db
                loadDataFromDB()
                TextBox1.Text = foreignCompanyVM.Id1.ToString
                'set cmb to new user
                loadDataFromDB()
                populateForm(foreignCompanyVM)
            Else
                Throw New Exception("Errorin Creating record")
            End If
        Catch ex As Exception
            Throw ex

        End Try

        Return retVal
    End Function

    Private Sub loadDataFromDB()
        Try
            Dim listCompanies As New List(Of ForeignCompanyViewModel)
            Dim listEmployees As New List(Of EmployeeViewModel)
            listEmployees = GetAllEmployees()
            'load cmb 
            cmbCompanyContact.Items.Clear()
            cmbCompanyContact.Items.Add("---Select ----")
            For Each item In listEmployees
                cmbCompanyContact.Items.Add(item)
            Next
            cmbCompanyContact.DisplayMember = "Name1"
            cmbCompanyContact.ValueMember = "Id1"

            listCompanies = GetAllCompaines()
            'load cmb 
            'cmbCompany.DataSource = listCompanies
            cmbCompany.Items.Clear()
            cmbCompany.Items.Add("---Select ----")
            For Each item In listCompanies
                cmbCompany.Items.Add(item)

            Next
            cmbCompany.DisplayMember = "Name1"
            cmbCompany.ValueMember = "Id1"
        Catch ex As Exception
            MsgBox("Error Loading Data FromDB.Please try again", MsgBoxStyle.OkOnly, "Save")
        End Try
    End Sub

    Private Function GetAllEmployees() As List(Of EmployeeViewModel)
        Dim empVM As New EmployeeViewModel
        Dim list As List(Of EmployeeViewModel) = empVM.SelectAll()
        Console.WriteLine(list.Count)
        Return list
    End Function

    Private Function GetAllCompaines() As List(Of ForeignCompanyViewModel)
        Dim empVM As New ForeignCompanyViewModel
        Dim list As List(Of ForeignCompanyViewModel) = empVM.SelectAll()
        Console.WriteLine(list.Count)
        Return list
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If validateEntry() Then
                'update
                If cmbCompany.SelectedIndex > 0 Then
                    Select Case MsgBox("Are You sure update this record?", MsgBoxStyle.YesNo, "Update")
                        Case MsgBoxResult.Yes



                            'update

                            Dim retVal As Integer = updateCompany()
                            If retVal >= 1 Then
                                MsgBox("Record was updated", MsgBoxStyle.OkOnly, "Update")
                            Else
                                MsgBox("Error in updating record.Please try again.If th error presist,contact your IT Dept", MsgBoxStyle.OkOnly, "Update")
                            End If
                        Case MsgBoxResult.No

                    End Select
                Else
                    'save
                    Select Case MsgBox("Are You sure create this record?", MsgBoxStyle.YesNo, "Create")
                        Case MsgBoxResult.Yes
                            Dim retVal As Integer = saveCompany()

                        Case MsgBoxResult.No

                    End Select
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try




    End Sub

    Private Sub ForeignCompanyFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadDataFromDB()
        If txtPhoneNum.Text.Length < 1 Then
            clearForm()
        End If


    End Sub
    Sub populateForm(foreignCompanyViewModel As ForeignCompanyViewModel)
        Try
            For Each comboItem In cmbCompany.Items
                Try
                    Dim t As Integer
                    If Integer.TryParse(comboItem.ID1, t) Then
                        Dim strID As String = CStr(comboItem.ID1)
                        If strID = CStr(foreignCompanyViewModel.Id1) Then
                            cmbCompany.SelectedItem = comboItem
                            Label1.Text = strID
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                Finally

                End Try
            Next

            For Each comboItem In cmbCompanyContact.Items
                Try
                    Dim t As Integer
                    If Integer.TryParse(comboItem.ID1, t) Then
                        Dim strID As String = CStr(comboItem.ID1)
                        If strID = CStr(foreignCompanyViewModel.EmployeeNum1) Then
                            cmbCompanyContact.SelectedItem = comboItem

                            txtCompanyName.Text = foreignCompanyViewModel.Name1
                            txtCity.Text = foreignCompanyViewModel.Name1
                            txtCountry.Text = foreignCompanyViewModel.Country1
                            txtPhoneNum.Text = foreignCompanyViewModel.Phonenum1
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                Finally

                End Try
            Next


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try
    End Sub
    Private Sub cmbCompany_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbCompany.SelectionChangeCommitted
        If cmbCompany.SelectedIndex >= 1 Then
            Dim viewModel As New ForeignCompanyViewModel

            Dim empID As Integer
            Try
                empID = cmbCompany.SelectedItem.Id1
                viewModel = viewModel.GetByID(empID)
                If viewModel.Id1 > 0 Then
                    loadDataFromDB()

                    populateForm(viewModel)
                Else
                    Throw New Exception("Invalid Company ID")
                End If
                'set empiD

                btnSave.Text = "Update"
            Catch ex As Exception
                btnSave.Text = "Save"
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
            Finally

            End Try
        Else
            btnSave.Text = "Save"
            clearForm()

        End If


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Select Case MsgBox("Are You sure you want to Delete This Record?", MsgBoxStyle.YesNo, "Confrim Delete")
            Case MsgBoxResult.Yes
                Dim companyID, retval As Integer
                retval = -1
                Try
                    If cmbCompany.SelectedIndex >= 1 Then
                        companyID = cmbCompany.SelectedItem.ID1
                        If companyID > 0 Then
                            Dim viewModel As New ForeignCompanyViewModel
                            viewModel = viewModel.GetByID(companyID)
                            If viewModel.Id1 >= 1 Then
                                retval = viewModel.Delete(viewModel.Id1)
                                If retval > 0 Then
                                    MsgBox("Company was deleted", MsgBoxStyle.OkOnly, "Record deleted")

                                    loadDataFromDB()
                                    clearForm()
                                    'populateForm(viewModel)
                                Else
                                    Throw New Exception("Error in Deleting company.try again")
                                End If
                            Else
                                Throw New Exception("Invalid Company ID:Company does not exist in DB")
                            End If
                        Else
                            Throw New Exception("Invalid Company ID")
                        End If
                    Else
                        Throw New Exception("Please select a valid company")
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Finally

                End Try
            Case MsgBoxResult.No

        End Select


    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        clearForm()

    End Sub

    Private Sub clearForm()
        cmbCompany.SelectedIndex = 0
        cmbCompanyContact.SelectedIndex = 0
        txtCity.Text = ""
        txtCompanyName.Text = ""
        txtCountry.Text = ""
        txtPhoneNum.Text = ""
        Label1.Text = "Company ID"
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Select Case MsgBox("Are You sure you want to Exit?", MsgBoxStyle.YesNo, "Exit")
            Case MsgBoxResult.Yes
                Me.Close()
            Case MsgBoxResult.No

        End Select
    End Sub


End Class