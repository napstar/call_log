Imports Call_LogApp

Public Class ForeignCompanyViewModel
    Implements IRepository(Of ForeignCompanyViewModel)

    Private id, EmployeeNum As Integer
    Private name, phonenum, city, country As String

    Public Property City1 As String
        Get
            Return city
        End Get
        Set(value As String)
            city = value
        End Set
    End Property

    Public Property Country1 As String
        Get
            Return country
        End Get
        Set(value As String)
            country = value
        End Set
    End Property

    Public Property EmployeeNum1 As Integer
        Get
            Return EmployeeNum
        End Get
        Set(value As Integer)
            EmployeeNum = value
        End Set
    End Property

    Public Property Id1 As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
        End Set
    End Property

    Public Property Name1 As String
        Get
            Return name
        End Get
        Set(value As String)
            name = value
        End Set
    End Property

    Public Property Phonenum1 As String
        Get
            Return phonenum
        End Get
        Set(value As String)
            phonenum = value
        End Set
    End Property



    Public Sub Save() Implements IRepository(Of ForeignCompanyViewModel).Save
        Throw New NotImplementedException()
    End Sub

    Public Function Insert(obj As ForeignCompanyViewModel) As Integer Implements IRepository(Of ForeignCompanyViewModel).Insert
        Dim retVal As Integer
        retVal = -1
        Try

            Using ctx As New CallLogDBEntities1
                Dim dto As New Foreign_Company
                dto = MapToDataObject(obj)
                dto.ID = -1
                ctx.Foreign_Company.Add(dto)
                Dim rec_count As Integer = ctx.SaveChanges()
                If rec_count > 0 Then
                    retVal = dto.ID
                Else
                    Throw New Exception("Error in Updating record")
                End If

            End Using

        Catch e As Entity.Validation.DbEntityValidationException
            For Each eve In e.EntityValidationErrors
                Console.WriteLine(eve.Entry.Entity.GetType().Name, eve.Entry.State)
                For Each ve In eve.ValidationErrors
                    Console.WriteLine(ve.PropertyName, ve.ErrorMessage)
                Next

            Next

        Catch ex As Exception

            Throw ex


        End Try
        Return retVal
    End Function

    Private Function MapToDataObject(obj As ForeignCompanyViewModel) As Foreign_Company
        Dim retVal As New Foreign_Company

        retVal.City = obj.City1
        retVal.Country = obj.Country1
        retVal.EmployeeNum = obj.EmployeeNum
        retVal.ID = obj.Id1
        retVal.Name = obj.Name1
        retVal.PhoneNumber = obj.Phonenum1

        Return retVal


    End Function

    Public Function SelectAll() As IEnumerable(Of ForeignCompanyViewModel) Implements IRepository(Of ForeignCompanyViewModel).SelectAll
        Dim retVal As New List(Of ForeignCompanyViewModel)
        Try
            Using ctx As New CallLogDBEntities1
                Dim listDTO As New List(Of Foreign_Company)
                listDTO = ctx.Foreign_Company.ToList()
                For Each dto As Foreign_Company In listDTO
                    Dim emv As New ForeignCompanyViewModel
                    emv = MapToViewModel(dto)
                    retVal.Add(emv)
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try


        Return retVal
    End Function

    Public Function GetByID(id As Integer) As ForeignCompanyViewModel Implements IRepository(Of ForeignCompanyViewModel).GetByID
        Dim retVal As New ForeignCompanyViewModel
        Using ctx As New CallLogDBEntities1

            Dim dataObject As Foreign_Company = (From c In ctx.Foreign_Company
                                                 Where c.ID = id
                                                 Select c).FirstOrDefault

            retVal = MapToViewModel(dataObject)
        End Using


        Return retVal
    End Function

    Private Function MapToViewModel(dataObject As Foreign_Company) As ForeignCompanyViewModel
        Dim retVal As New ForeignCompanyViewModel
        retVal.Id1 = dataObject.ID
        retVal.Name1 = dataObject.Name
        retVal.Phonenum1 = dataObject.PhoneNumber
        retVal.Country1 = dataObject.Country
        retVal.City1 = dataObject.City
        retVal.EmployeeNum1 = dataObject.EmployeeNum


        Return retVal

    End Function

    Public Function Update(obj As ForeignCompanyViewModel) As Integer Implements IRepository(Of ForeignCompanyViewModel).Update
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New Foreign_Company
                dto = MapToDataObject(obj)

                ctx.Entry(dto).State = Entity.EntityState.Modified
                retVal = ctx.SaveChanges()

            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try



        Return retVal
    End Function

    Public Function Delete(id As Integer) As Object Implements IRepository(Of ForeignCompanyViewModel).Delete
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New Foreign_Company

                dto = ctx.Foreign_Company.Find(id)
                If dto.ID > 0 Then
                    ctx.Foreign_Company.Remove(dto)
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

    Public Function GetCompanyByPhoneNum(strPhoneNum As String)
        Dim retVal As New ForeignCompanyViewModel
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New Foreign_Company
                Dim count As Integer
                'does it exists?
                count = (From c In ctx.Foreign_Company
                         Where c.PhoneNumber = strPhoneNum
                         Select c).Count
                If count > 0 Then
                    dto = (From c In ctx.Foreign_Company
                           Where c.PhoneNumber = strPhoneNum
                           Select c).FirstOrDefault
                    retVal = MapToViewModel(dto)

                End If





            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return retVal
    End Function
End Class
