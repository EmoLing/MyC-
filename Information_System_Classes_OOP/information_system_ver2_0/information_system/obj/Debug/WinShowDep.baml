   M S B A M L   `   `   `  ����M  Iinformation_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:information_systeminformation_system  5   	   #    information_system.WinShowDep   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:information_system     5   	     � Title$  
WinShowDep��5   	   $	��450��6   $
��1100��6)    � 
ResizeMode$ NoResize=�66   .��5	      � .+�5
   
   �� -   5         ��Name$ listDep��5
      $
��Left=�6$   $	��400��6?   $��	10,10,0,0q�6L   $	��Top=�6_   $	��465��6w    �� View 5      �� 5      .P�5      ��  �� Width$ 60��6N    �� DisplayMemberBinding 6%   ��  �� Path$ ID�.��6Z   №5      �� 5      $��Название��6%   $	 200��6b    67   �� $
 Name�5      �� 5      $	 200��6X    6%   �� $ DateOfCreate�.��6d   Дата Создания5      5      5      �� 5   
   -   5        $ 
listWorker��5      $
��Left=�6'   $	��400��6B   $��
480,10,0,0q�6O   $	��Top=�6c   $	��487��6{    5      �� 5      .P�5      �� $ 40��6N    6%   �� $ ID�.��6Z   №5      �� 5      $	 100��6U    6%   �� $ 	FirstName�.��6a   Имя5       �� 5!      $	 100��6T    6%   �� $ LastName�.��6`   Фамилия5#      �� 5$      $
 Auto��6O    6%   �� $	 Age�.��6\   Возраст5&      �� 5'      $
 Auto��6V    6%   �� $ 
Department�.��6c   
Отдел5)      �� 5*      $
 Auto��6T    6%   �� $ Position�.��6a   Должность5,      �� 5-      $
 Auto��6R    6%   �� $ Salary�.��6_   Зарплата в $5/      50      52      �� 53   
   -   5        $��ButShowWorkers��53      $��Показать��6'   $
��Left=�6:   $��
972,10,0,0q�6U   $	��Top=�6i   $	��112��6�   $��29��6�   6�   56      57      