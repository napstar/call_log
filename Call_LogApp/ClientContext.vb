Imports System.Data.Entity

Public Class ClientContext
    Inherits DbContext
    Public Overridable Property Employees As DbSet(Of Employee)
        Get
        End Get
        Set
        End Set
    End Property
End Class


