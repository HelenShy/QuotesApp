   M S B A M L   `   `   `  ����D  @QuotesApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QuotesApp	QuotesApp  5   	       QuotesApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QuotesApp     5   	     � Title$  
MainWindow��5   	   $	��250��6   $	��600��6)   .��5
      � ��6   ��  �� Path$ AppQuote.QuoteColor� �� Mode$ OneWay=� � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� -   5         ��Name$ 	QuoteText��5      $��1��6'    �� TextAlignment$ Center=�64   $��Stretch=�6L   $
��Bold"�6h   $��Italic$�6z   $
��16px(�6�   $
��Wrap=�6�   $��Segoe UI Emoji)�6�   ��6�   �� $ AppQuote.CurrentQuote�$ OneWay=�6  �� 5   
   -   5         ��Name$ MyImage��5      $��1��6!   $��3��61   $��Stretch=�6C   ��6a   �� $ AppQuote.MyImg�$ TwoWay=�6�   5      5      