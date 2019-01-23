Public Interface IRepository(Of T As Class)
    Function SelectAll() As IEnumerable(Of T)
    Function GetByID(ByVal id As Integer) As T
    Function Insert(ByVal obj As T) As Integer
    Function Update(ByVal obj As T) As Integer
    Function Delete(ByVal id As Integer)
    Sub Save()
End Interface
