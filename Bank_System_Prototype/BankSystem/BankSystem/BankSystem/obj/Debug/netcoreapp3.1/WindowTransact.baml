   M S B A M L   `   `   `  ����E  ABankSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Bank_SystemBank_System  5   	        Bank_System.WindowTransact   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Bank_System     5   	   -   5          � Title$  WindowTransact��5   	   $	��232��6    $	��300��6-   .��5	      � .+�5
   
   �� $��
Отдел��6   $
��Left=�6    $��	10,10,0,0q�6;   $	��Top=�6N   �� 5   
   -   5         ��Name$ ComboDepartment��5      $
��Left=�6,   $��
121,10,0,0q�6G   $	��Top=�6[   $	��124��6s   $��26��6    �� SelectedIndex$ 0��6�   .@�5      �� -   5         ��Name$ ItemNatural��5      $��Физический��60   �� 5      -   5        $ 	ItemLegal��5      $��Юридический��6.   �� 5      -   5        $ ItemVIP��5      $	��VIP��6,   5      �� 5   
   $��Номер счета��6   $
��Left=�6&   $��	10,54,0,0q�6A   $	��Top=�6T   $��85��6l   �� 5   
   -   5         ��Name$ BoxAccountNumber��5      $
��Left=�6-   $��
123,54,0,0q�6H    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $	��122��6�   $��26��6�   6�   �� 5   
   -   5         ��Name$ 	LabelName��5      $
��Left=�6#   $��	15,92,0,0q�6>   $	��Top=�6Q   $	��230��6i   $��23��6u   �� 5   
   -   5         ��Name$ ButTransact��5      $��Перевести��6&   $
��Left=�6:   $��
15,158,0,0q�6U   $	��Top=�6i   $��29��6�   $��71��6�   6�   �� 5   
   -	   5        $ 	ButCancel��5      $��Отмена��6$   $
��Left=�65   $��121,158,0,0q�6P   $	��Top=�6e   $��29��6}   $��71��6�   6�   �� 5   
   -
   5        $ 	ButSearch��5      $��Button��6$   $
��Left=�65   $��
250,54,0,0q�6P   $	��Top=�6d   $��21��6|   $��27��6�   6�   �� 5   
   $��
Сумма��6   $
��Left=�6!   $��
15,127,0,0q�6<   $	��Top=�6P   $��71��6h   �� 5   
   -   5        $ BoxSum��5      $
��Left=�6"   $��121,132,0,0q�6=   $��0��6R   $
 Wrap=�6[   $	��Top=�6o   $	��129��6�   $��21��6�   6�   5      5      