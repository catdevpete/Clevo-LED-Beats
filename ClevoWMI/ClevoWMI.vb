Imports System.Management

Public Class ClevoWMI

	Private strComputer As String
	Private options As ConnectionOptions
	Private myScope As ManagementScope
	Private oQuery As SelectQuery
	Private oResults As ManagementObjectSearcher

	Private keyboardSection As String()
	Private colors As Int32()
	Private colorUInt As UInt32

	Public Sub New()
		strComputer = "."
		options = New ConnectionOptions With {
			.Impersonation = ImpersonationLevel.Impersonate,
			.EnablePrivileges = True
		}
		myScope = New ManagementScope("\\" & strComputer & "\root\wmi", options)
		oQuery = New SelectQuery("Select * from CLEVO_GET")
		oResults = New ManagementObjectSearcher(myScope, oQuery)
		keyboardSection = New String() {"F0", "F1", "F2"}
	End Sub

	Public Sub SetColor(ByVal value As String)
		Dim oItem As ManagementObject
		For Each oItem In oResults.Get()
			For Each oProperty In oItem.Properties
				Try
					Dim taskA = Task.Factory.StartNew(Sub()
														  colorUInt = Convert.ToUInt32(value, 16)
														  oItem.InvokeMethod("SetKBLED", New Object() {colorUInt})
													  End Sub)
				Catch ex As Exception
					Continue For
				End Try
			Next
		Next
	End Sub

	Public Sub SetColorAll(ByVal value As String)
		Dim oItem As ManagementObject
		For Each oItem In oResults.Get()
			For Each oProperty In oItem.Properties
				Try
					Parallel.ForEach(keyboardSection, Sub(section)
														  colorUInt = Convert.ToUInt32(section + value, 16)
														  oItem.InvokeMethod("SetKBLED", New Object() {colorUInt})
													  End Sub)
				Catch ex As Exception
					Continue For
				End Try
			Next
		Next
	End Sub

End Class