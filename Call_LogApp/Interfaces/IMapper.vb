Public Interface IMapperOf(Of T As Class, K As Class)
    Function MapToDataObject(K) As Employee
    Function MapToViewModel(T) As EmployeeViewModel
End Interface
