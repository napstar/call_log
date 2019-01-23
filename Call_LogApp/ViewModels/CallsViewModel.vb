Imports Call_LogApp

Public Class CallsViewModel
    Implements IRepository(Of CallsViewModel)
    Dim id As Integer
    Dim _date As Date
    Dim phoneNum As String
    Dim duration As Integer

    Dim employeeID As Integer
    Dim employeeName As String
    Dim companyID As Integer
    Dim companyName As String


    Public Property Id1 As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
        End Set
    End Property

    Public Property [Date] As Date
        Get
            Return _date
        End Get
        Set(value As Date)
            _date = value
        End Set
    End Property

    Public Property PhoneNum1 As String
        Get
            Return phoneNum
        End Get
        Set(value As String)
            phoneNum = value
        End Set
    End Property

    Public Property Duration1 As Integer
        Get
            Return duration
        End Get
        Set(value As Integer)
            duration = value
        End Set
    End Property

    Public Property EmployeeID1 As Integer
        Get
            Return employeeID
        End Get
        Set(value As Integer)
            employeeID = value
        End Set
    End Property

    Public Property EmployeeName1 As String
        Get
            Return employeeName
        End Get
        Set(value As String)
            employeeName = value
        End Set
    End Property

    Public Property CompanyID1 As Integer
        Get
            Return companyID
        End Get
        Set(value As Integer)
            companyID = value
        End Set
    End Property

    Public Property CompanyName1 As String
        Get
            Return companyName
        End Get
        Set(value As String)
            companyName = value
        End Set
    End Property

    Public Function SelectAll() As IEnumerable(Of CallsViewModel) Implements IRepository(Of CallsViewModel).SelectAll
        Dim retVal As New List(Of CallsViewModel)
        Try
            Using ctx As New CallLogDBEntities1
                Dim listDTO As New List(Of [Call])
                listDTO = ctx.Calls.ToList()
                For Each dto As [Call] In listDTO
                    Dim emv As New CallsViewModel
                    emv = MapToViewModel(dto)
                    retVal.Add(emv)
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try


        Return retVal
    End Function

    Private Function MapToViewModel(dto As [Call]) As CallsViewModel
        Dim retVal As New CallsViewModel

        Try
            retVal.Id1 = dto.ID
            retVal.Date = dto.Date
            retVal.PhoneNum1 = dto.PhoneNumber
            retVal.Duration1 = dto.Duration
            retVal.CompanyID1 = dto.ForeignCompanyID
            retVal.CompanyName1 = dto.ForeignCompanyName
            retVal.EmployeeID1 = dto.EmployeeID
            retVal.EmployeeName1 = dto.EmployeeName


        Catch ex As Exception
            Throw ex

        End Try

        Return retVal

    End Function

    Public Function GetByID(id As Integer) As CallsViewModel Implements IRepository(Of CallsViewModel).GetByID
        Dim retVal As New CallsViewModel
        Try
            Using ctx As New CallLogDBEntities1

                Dim dataObject As [Call] = ctx.Calls.Find(id)
                retVal = MapToViewModel(dataObject)
            End Using
        Catch ex As Exception
            Throw ex


        End Try



        Return retVal
    End Function

    Public Function Insert(obj As CallsViewModel) As Integer Implements IRepository(Of CallsViewModel).Insert
        Dim retVal As Integer
        retVal = -1
        Try

            Using ctx As New CallLogDBEntities1
                Dim dto As New [Call]
                dto = MapToDataObject(obj)
                dto.ID = -1
                ctx.Calls.Add(dto)
                ctx.SaveChanges()
                retVal = dto.ID
            End Using

        Catch e As Entity.Validation.DbEntityValidationException
            For Each eve In e.EntityValidationErrors
                Console.WriteLine(eve.Entry.Entity.GetType().Name, eve.Entry.State)
                For Each ve In eve.ValidationErrors
                    Console.WriteLine(ve.PropertyName, ve.ErrorMessage)
                Next

            Next
            Throw e



        Catch ex As Exception

            Throw ex


        End Try
        Return retVal
    End Function

    Private Function MapToDataObject(obj As CallsViewModel) As [Call]
        Dim retVal As New [Call]

        Try
            retVal.ID = obj.Id1
            retVal.Date = obj.Date
            retVal.PhoneNumber = obj.PhoneNum1
            retVal.Duration = obj.Duration1

            retVal.ForeignCompanyID = obj.CompanyID1

            retVal.ForeignCompanyName = obj.CompanyName1
            retVal.EmployeeID = obj.EmployeeID1
            retVal.EmployeeName = obj.EmployeeName1


        Catch ex As Exception
            Throw ex

        End Try

        Return retVal
    End Function

    Public Function Update(obj As CallsViewModel) As Integer Implements IRepository(Of CallsViewModel).Update
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New [Call]
                dto = MapToDataObject(obj)

                ctx.Entry(dto).State = Entity.EntityState.Modified
                retVal = ctx.SaveChanges()

            End Using
        Catch ex As Exception
            Throw ex


        End Try



        Return retVal
    End Function

    Public Function Delete(id As Integer) As Object Implements IRepository(Of CallsViewModel).Delete
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New [Call]

                dto = ctx.Calls.Find(id)
                If dto.ID > 0 Then
                    ctx.Calls.Remove(dto)
                    retVal = ctx.SaveChanges()
                Else
                    Throw New Exception("Invalid ID Error.The Employee does not exist")
                End If


            End Using
        Catch ex As Exception
            Throw ex
        End Try



        Return retVal
    End Function

    Public Sub Save() Implements IRepository(Of CallsViewModel).Save
        Throw New NotImplementedException()
    End Sub
End Class
