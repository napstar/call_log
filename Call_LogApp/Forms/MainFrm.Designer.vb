<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainFrm

    Inherits System.Windows.Forms.Form



    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmployeeMaintenanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForeignCompanyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CallsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(790, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmployeeMaintenanceToolStripMenuItem, Me.ForeignCompanyToolStripMenuItem, Me.CallsToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'EmployeeMaintenanceToolStripMenuItem
        '
        Me.EmployeeMaintenanceToolStripMenuItem.Name = "EmployeeMaintenanceToolStripMenuItem"
        Me.EmployeeMaintenanceToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.EmployeeMaintenanceToolStripMenuItem.Text = "Employee Maintenance"
        '
        'ForeignCompanyToolStripMenuItem
        '
        Me.ForeignCompanyToolStripMenuItem.Name = "ForeignCompanyToolStripMenuItem"
        Me.ForeignCompanyToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ForeignCompanyToolStripMenuItem.Text = "Foreign Company"
        '
        'CallsToolStripMenuItem
        '
        Me.CallsToolStripMenuItem.Name = "CallsToolStripMenuItem"
        Me.CallsToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.CallsToolStripMenuItem.Text = "Calls"
        '
        'MainFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(790, 422)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainFrm"
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EmployeeMaintenanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ForeignCompanyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CallsToolStripMenuItem As ToolStripMenuItem
End Class
