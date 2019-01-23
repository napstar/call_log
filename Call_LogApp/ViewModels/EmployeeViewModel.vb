Imports Call_LogApp
Imports AutoMapper

Public Class EmployeeViewModel
    Implements IRepository(Of EmployeeViewModel)


    Private id As Integer?
    Private name As String
    Private extension As String
    Private callLogList As New List(Of CallsViewModel)
    Private context As CallLogDBEntities1
    Public Sub New()

    End Sub
    Public Sub New(id As Integer, name As String, extension As String)
        Me.id = id
        Me.name = name
        Me.extension = extension
    End Sub

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

    Public Property Extension1 As String
        Get
            Return extension
        End Get
        Set(value As String)
            extension = value
        End Set
    End Property
    Function GetExtensionCount(strExtentionNum As String) As Integer
        Dim empList As New List(Of EmployeeViewModel)
        Dim retVal As Integer
        retVal = -1
        Try
            empList = SelectAll()
            retVal = Aggregate c In empList
                                    Where c.Extension1 = strExtentionNum
                                        Into Count()

        Catch ex As Exception
            Throw ex
        End Try
        Return retVal

    End Function
    'Function GetByID(id As Integer) As EmployeeViewModel

    '    Dim retVal As New EmployeeViewModel
    '    Using ctx As New CallLogDBEntities1

    '        Dim dataObject As Employee = ctx.Employees.Find(id)
    '        retVal = MapToViewModel(dataObject)
    '    End Using


    '    Return retVal
    'End Function






    Public Function MapToDataObject(empDTO As EmployeeViewModel)

        Dim retVal As New Employee

        retVal.ID = empDTO.Id1
        retVal.Name = empDTO.Name1
        retVal.Extension = empDTO.Extension1

        Return retVal

    End Function

    Public Function MapToViewModel(dto As Employee)

        Dim retVal As New EmployeeViewModel
        retVal.Id1 = dto.ID
        retVal.Name1 = dto.Name
        retVal.Extension1 = dto.Extension



        Return retVal
    End Function

    Public Function SelectAll() As IEnumerable(Of EmployeeViewModel) Implements IRepository(Of EmployeeViewModel).SelectAll
        Dim retVal As New List(Of EmployeeViewModel)
        Try
            Using ctx As New CallLogDBEntities1
                Dim listDTO As New List(Of Employee)
                listDTO = ctx.Employees.ToList()
                For Each dto As Employee In listDTO
                    Dim emv As New EmployeeViewModel
                    emv = MapToViewModel(dto)
                    retVal.Add(emv)
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try


        Return retVal
    End Function

    Public Function GetByID(id As Integer) As EmployeeViewModel Implements IRepository(Of EmployeeViewModel).GetByID
        Dim retVal As New EmployeeViewModel
        Try
            Using ctx As New CallLogDBEntities1

                Dim dataObject As Employee = ctx.Employees.Find(id)
                If dataObject.ID > 0 Then
                    retVal = MapToViewModel(dataObject)
                End If

            End Using
        Catch ex As Exception
            Throw ex


        End Try



        Return retVal
    End Function

    Public Function Insert(obj As EmployeeViewModel) As Integer Implements IRepository(Of EmployeeViewModel).Insert
        Dim retVal As Integer
        retVal = -1
        Try

            Using ctx As New CallLogDBEntities1
                Dim dto As New Employee
                obj.id = -1
                dto = MapToDataObject(obj)

                ctx.Employees.Add(dto)
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

        Catch ex As Exception

            Throw ex


        End Try
        Return retVal
    End Function

    Public Function Update(obj As EmployeeViewModel) As Integer Implements IRepository(Of EmployeeViewModel).Update
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New Employee
                dto = MapToDataObject(obj)

                ctx.Entry(dto).State = Entity.EntityState.Modified
                retVal = ctx.SaveChanges()

            End Using
        Catch ex As Exception
            Throw ex
        Finally

        End Try



        Return retVal
    End Function



    Public Sub Save() Implements IRepository(Of EmployeeViewModel).Save
        Throw New NotImplementedException()
    End Sub

    Public Function Delete(id As Integer) As Object Implements IRepository(Of EmployeeViewModel).Delete
        Dim retVal As Integer
        retVal = -1
        Try
            Using ctx As New CallLogDBEntities1
                Dim dto As New Employee

                dto = ctx.Employees.Find(id)
                If dto.ID > 0 Then
                    'does this employee have any calls
                    Dim count As Integer = (From c In ctx.Calls
                                            Where c.EmployeeID = dto.ID
                                            Select c).Count
                    If count >= 1 Then
                        'delete call logs first
                        Dim listCall As New List(Of [Call])
                        listCall = (From d In ctx.Calls
                                    Where d.EmployeeID = dto.ID
                                    Select d).ToList()
                        For Each item In listCall
                            ctx.Entry(item).State = Entity.EntityState.Deleted
                        Next

                    End If
                    ctx.Entry(dto).State = Entity.EntityState.Deleted

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
End Class
