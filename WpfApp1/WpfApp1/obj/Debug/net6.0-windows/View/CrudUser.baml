   M S B A M L   `   `   `  ����R  NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data  5   	    WpfApp1   WpfApp1.View.CrudUser   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ?data.clr-namespace:System.Data;assembly=System.Data     5   	     � Title$  User Management��5   	   $	��450��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�5   	   
����ح�.��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   ���#F4F4F4.+�5      �� -   5        $��	BooK Crud��5      $��10q�6)   
�������
�������6Z   �� 5      $��	User Crud��6   $��10q�6)   
�������
�������6G   �� 5      $
��Book��6   $��10q�6$   
�������
�������6B   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $��1��6%   $��10,150,10,10q�65      AutoGenerateColumns$
 True��6K   �� 5   
   $��1��6   $��	Full Name��6    $
��Left=�64   $��	29,30,0,0q�6O   $	��Top=�6b   �� 5   
   $��1��6   $��Phone Number��6    $
��Left=�67   $��	29,56,0,0q�6R   $	��Top=�6e   �� 5   
   $��1��6   $��Status��6    $
��Left=�61   $��	29,82,0,0q�6L   $	��Top=�6_   �� 5   
   -   5         ��Name$ textBox1��5      $��1��6$   $
��Left=�64   $��
135,34,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6x   $	��120��6�   �� 5   
   -   5        $ textBox2��5      $��1��6$   $
��Left=�64   $��
135,64,0,0q�6O   $
 Wrap=�6c   $	��Top=�6x   $	��120��6�   �� 5   
   -   5         ��Name$ 	ComboBox1��5      $��1��6&   $
��Left=�66   $��
135,87,0,0q�6Q   $	��Top=�6e   $	��120��6}   .@�5       �� $��Active��6   �� 5!      $��InActive��6   5"      �� 5%   
   -   5        $��1��5%      $��INSERT��6!   $
��Left=�62   $��
58,127,0,0q�6M   $	��Top=�6a   
�������
�������6�   �� 5&   
   -   5        $��1��5&      $��UPDATE��6!   $
��Left=�62   $��117,127,0,0q�6M   $	��Top=�6b   
�������
�������6�   �� 5'   
   -   5        $��1��5'      $��DELETE��6!   $
��Left=�62   $��182,127,0,0q�6M   $	��Top=�6b   
�������
�������6�   �� 5(   
   -	   5        $ textBoxSearch��5(      $��1��6)   $
��Left=�69   $��475,129,0,0q�6T   $
 Wrap=�6i   $	��Top=�6~   $	��120��6�   �� 5)   
   -
   5        $��1��5)      $��Search��6!   $
��Left=�62   $��612,127,0,0q�6M   $	��Top=�6b   
�������
�������6�   5*      5+      