   M S B A M L   `   `   `  ����E  ABankSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Bank_SystemBank_System  5   	       Bank_System.WindowCredit   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Bank_System     5   	   -   5          � Title$  WindowCredit��5   	   $	��400��6   $	��400��6+   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $	��62* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��220 �6   �� 5      $	��31* �6   .+�5   
   �� $��
Сумма��6   $
��Left=�6    $��Center=�6;   $��1��6V   $��30��6f   $��56��6r   $��1��6}   $��10,0,0,0q�6�   �� 5   
   $)��#Количество месяцев��6   $��1��6-   $��Center=�6=   $��2��6Z   $��Center=�6g   $��30��6�   $	��200��6�   �� 5   
   -   5         ��Name$ BoxSum��5      $��2��6"   $��Center=�62   $��1��6O    �� TextWrapping$
 Wrap=�6\   $��Center=�6p   $	��146��6�   $��30��6�   6�   �� 5   
   -   5        $ BoxCountMonth��5      $��2��6)   $��Center=�69   $��2��6V   $
 Wrap=�6d   $��Center=�6x   $	��146��6�   $��30��6�   6�   �� 5   
   $-��'Первоначальный взнос��6   $��1��6/   $��Center=�6?   $��3��6\   $��Center=�6i   $��30��6�   $	��200��6�   �� 5   
   -   5        $ BoxFirstSum��5      $��2��6'   $��Center=�67   $��3��6T   $
 Wrap=�6a   $��Center=�6u   $	��146��6�   $��30��6�   ${�False��6�   �� 5   
   $)��#Ежемесячный платеж��6   $��Center=�6-   $��4��6J   $��Center=�6W   $��1��6r   $��30��6�   $	��200��6�   �� 5   
   -   5        $ BoxEvermonth��5      $��2��6)   $��Center=�69   $��4��6V   $
 Wrap=�6d   $��Center=�6x   $	��146��6�   $��30��6�   ${�False��6�   �� 5    
   $:��4Сумма долга (если не платить)��6   $��1��68   $��Center=�6H   $��5��6e   $��Center=�6r   $��30��6�   $	��200��6�   �� 5!   
   -   5        $ BoxDebt��5!      $��2��6#   $��Center=�63   $��5��6P   $
 Wrap=�6^   $��Center=�6r   $	��146��6�   $��30��6�   ${�False��6�   �� 5"   
   -   5         ��Name$ 
ButSeeTest��5"      $1��+Предварительный расчет��6%   $��1��6F   $
��Left=�6V   $��10,9,0,0q�6q   $��6��6�   $	��Top=�6�   $	��160��6�   $��28��6�   6�   �� 5#   
   -   5        $ 	ButCredit��5#      $��Оформить��6$   $��1��67   $
��Left=�6G   $��	10,62,0,0q�6b   $��6��6u   $	��Top=�6�   $��31��6�   $��70��6�   6�   �� 5$   
   -	   5        $ 	ButCancel��5$      $��Отмена��6$   $��1��65   $
��Left=�6E   $��	99,63,0,0q�6`   $��6��6s   $	��Top=�6�   $��30��6�   $��71��6�    �� IsCancel$
 True��6�   �� 5%   
   -
   5        $ BoxTestTime��5%      $��2��6'   $
��Left=�67   $��
102,10,0,0q�6R   $��6��6f   $
 Wrap=�6t   $	��Top=�6�   $��53��6�   $��28��6�   6�   �� 5&   
   $��+ к DateNow��6   $��2��6&   $
��Left=�66   $��	10,10,0,0q�6Q   $��6��6d   $	��Top=�6q   $��28��6�   $��87��6�   5(      5)      