Imports System.Windows.Forms
Imports System.Data
Imports Oracle.DataAccess.Client ' ODP.NET Oracle managed provider
Imports Oracle.DataAccess.Types



Public Class Form1

    Dim ds As DataSet


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ustawnienie wlasciwosci na mozliwosc wlasnego rysowania na tabControl
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed

    End Sub


    Sub pobierz_dane_z_bazy(sql As String)

        Dim da As OracleDataAdapter
        Dim cb As OracleCommandBuilder

        Dim dr As OracleDataReader

        Dim oradb As String = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" _
                    + "(ADDRESS=(PROTOCOL=TCP)(HOST=ENCELADUS.energa.loc)(PORT=1521)))" _
                    + "(CONNECT_DATA=(SERVICE_NAME=SRV_ERGH)));" _
                    + "User Id=skome;Password=szafran1;"

        Dim conn As New OracleConnection(oradb) ' Visual Basic
        conn.Open()

        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.CommandText = "select id, name, I_OBJECTEX_1,IDENT5 from counter where id>31400"

        'cmd.CommandText = ""

        cmd.CommandText = sql

        If CheckBox1.Checked = True Then
            cmd.CommandText = cmd.CommandText + "and OBJECT='Update_Kinga'"
        End If

        'If CheckedListBox1.Items(0) = True Then
        '    cmd.CommandText = cmd.CommandText + "and DIRECTION='oddanie'"
        'End If

        cmd.CommandType = CommandType.Text

        dr = cmd.ExecuteReader()


        da = New OracleDataAdapter(cmd)
        cb = New OracleCommandBuilder(da)
        ds = New DataSet()

        da.Fill(ds)



        dr.Dispose()
        cmd.Dispose()
        conn.Dispose()





    End Sub




    'zamiana orientacji tekstu na tabControl1 z pionowego na poziomy oraz podswitelenie aktywnego taba
    Private Sub TabControl1_DrawItem(ByVal sender As System.Object,
            ByVal e As DrawItemEventArgs) Handles TabControl1.DrawItem

        e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds)

        If TabControl1.SelectedIndex = e.Index Then 'Selected Tab
            e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds) '<--- My modification
        Else
            'zmiana koloru niezaznaczonych tabPage
            'e.Graphics.FillRectangle(New SolidBrush(Color.LightBlue), e.Bounds)
        End If

        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center
        e.Graphics.DrawString(TabControl1.TabPages(e.Index).Text,
                TabControl1.Font, SystemBrushes.ControlText,
                RectangleF.op_Implicit(e.Bounds), sf)

    End Sub

    'zaladuj dane do wykresu
    Sub wykresy()

        ''uzupełnenie danych dla serii wykresu
        'dr = cmd.ExecuteReader()
        'Me.Chart1.Series(0).Name = "moc"

        ''wyczyszczenie danych dla wykresu
        'Me.Chart1.Series("moc").Points.Clear()

        'While dr.Read()
        '    'Chart1.Series(0).Points.AddXY(dr.Item("name"), dr.Item("ident5"))

        '    Me.Chart1.Series("moc").Points.AddXY(dr.Item("name"), dr.Item("ident5")) 'rownoznaczne odwolanie (0)=("moc")

        'End While


        'Me.Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1.0R
    End Sub

    'zaladuj dane do listbox
    Sub lista_danych()
        '*********** załadowanie danych do labela
        'dr.Read()  ' replace this statement in next lab
        'Label1.Text = dr["name"].ToString();'dr.Item("dname") ' or dr.Item(0), remove in next lab
        'Label1.Text = dr.Item("name").ToString()

        '*********** załadowanie danych do listy
        'While dr.Read()
        '    Me.ListBox1.Items.Add(dr.Item("id").ToString() + "   PPE o nr " + dr.Item("name") +
        '                   " to zrodlo " + dr.Item("I_OBJECTEX_1") + " o mocy " + dr.Item("IDENT5") + " kW")
        'End While
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim sql As String

        'Dim i_wierszy As Integer
        'Dim i_kolumn As Integer
        'Dim row0 As String() = {"11/22/1968", "29", "Revolution 9",
        '    "Beatles", "The Beatles [White Album]"}

        sql = "select id, name, I_OBJECTEX_1,IDENT5 ob from formula where name like'MDM%' and calcid=24"
        sql = "select id, name, I_OBJECTEX_1,IDENT5 from counter where id>31400"
        pobierz_dane_z_bazy(sql)


        'i_kolumn = ds.Tables(0).Columns.Count
        'i_wierszy = ds.Tables(0).Rows.Count
        Me.DataGridView2.DataSource = ds.Tables(0)
        Me.Label1.Text = Me.Label1.Text + " " + ds.Tables(0).Rows.Count.ToString

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Dim form As New Form2



        Dim aa As Integer

        aa = Me.DataGridView2.CurrentRow.Cells(0).Value.ToString()

        form.Label1.Text = DataGridView2.CurrentRow.Cells(0).Value.ToString()
        form.Label2.Text = DataGridView2.CurrentRow.Cells(1).Value.ToString()
        form.Label3.Text = DataGridView2.CurrentRow.Cells(2).Value.ToString()
        form.Label4.Text = DataGridView2.CurrentRow.Cells(3).Value.ToString()

        form.ShowDialog()


    End Sub
End Class
