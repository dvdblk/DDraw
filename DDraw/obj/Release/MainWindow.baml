   M S B A M L   `   `   `  ���� @  <DDraw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DDrawDDraw      DDraw.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:DDraw       � Title$  DDraw��$	��600��$	��800�� � 	Resources    addLayerImage �� 	UriSource  visibleLayerImage  invisibleLayerImage  brushToolImage  lockedLayerImage  unlockedLayerImage  WinGray  ListBox.Static.Background �� Color  ListBox.Static.Border 	 ListBox.Disabled.Background 
 ListBox.Disabled.Border �� 
TargetType  DefaultButton �� Property �� Value��%�  &	        &	 #     &	 J     &	 s     &	 �     &	 �     &	 �     &	 �     &	     &		 %    &	
 :    &	 O    �� $ /Resources/layerAdd.png?��� $! /Resources/layerVisible.png?��� $# /Resources/layerInvisible.png?��� $# /Resources/brushToolImage.png?��� $  /Resources/layerLocked.png?��� $" /Resources/layerUnlocked.png?���#F0F0F0���� $ 	#FFFFFFFF���� $ 	#FFABADB3���� $ 	#FFFFFFFF���� $ 	#FFD9D9D9���� " ��.���  � ��
 �����.��]� .+�o� $	��Top=�.@�m� $��_File��.@�m� -    m�Name$ menuItemNew��$��New ...��m� -   $ menuItemOpen��$��_Open��m� -   $ menuItemSave��$��_Save���� $��Bottom=�.@��� -    ��Name$ mainImageCursorPosition��� 	 � ColumnDefinitions	 �� -   
 �Name$

 col1��$��3* ��� $��5 ��� $��* �$	��125��$	��400��.+�� 
�������	 �� $��40 ��� $��* �.+��� -    ��Name$ 	toolsList��
����������� " ��.���  � ��$ 30����  � ��
 �������  � ��$ Center=����� .R��� ���� *ToolImageName+�� -    ��Name$ mainImageScrollViewer��$��1����. $
��Auto=�$
��Auto=�.���� -    ��Name$ 	mainImage��$��0��$��0��$��Center=�$��Center=��� $��1��$��5��$��Stretch=�� $��2��#���   � RowDefinitions �� $��* ��� $��5 ��� $��2* �	 �� $��* ��� $��5 �.+�� -	    �Name$ additionalSettingsGrid��$��0��
������� �� -
    ��Name$ lineWidthRow��$��30 ��� -   $ strokeColorRow��$��30 ��� -   $ fillColorRow��$��30 ��� -   $ roundedCornersRow��$��30 ��� $��* �.+�� $��0��	 �� $��* ��� $��40 ��� $��10 ��� $��* ��� $��* �.+��� $��1������ *PenWidthBitmap+�� $��20��$��3������ *
PenWidth+�� $
��Left=�$��Center=�$��4��$��px��� $��1��	 �� $��* ��� $
��Auto ��� $��10 ��� $��* ��� $��* �.+��� $
��Left=�$��Center=�$��1��$��Stroke color:���� -    ��Name$ stokeColorBtn��$��3��#��[ .���� ���� *StrokeColorBitmap+� $��2��	 �� $��* ��� $
��Auto ��� $��10 ��� $��* ��� $��* �.+��� $
��Left=�$��Center=�$��1��$��Fill color:���� -   $ fillColorBtn��$��3��#��[ .���� ���� *FillColorBitmap+� $��2��
�������	 �� $	��63* ��� $
��134* � �� $��30 ��� $��* �.+�� $��2��	 �� $��30 ��� $
��Auto ��� $��30 ��� $��* ��� $��30 �.+��� -   $ addLayerBtn��$��0��#��[ {��� *ImageCreated+.���� #��[  �� $��1���� -   $ 
layersList��$��1��
�������$��2�� � ContextMenu �� .@�m� $
��Move��.@�m� -   $ layersListItemMoveUp��$��	Move 1 up��m� -   $ layersListItemMoveDown��$��Move 1 down���� m� -   $ layersListItemMoveTop��$��Move to top��m� -   $ layersListItemMoveBottom��$��Move to bottom���� m� -   $ layersListItemRemove��$��Delete selected layer������ " ��.���  � ��$ 50����  � ��
 ��������� .R�� 	 �� $��30 ��� $��5 ��� $��30 ��� $��5 ��� $��* ��� $��5 ��� $��30 �.+��� $ layerVisibleBtn��-   $��0��#��[ .���� ���� " �� �� Triggers ��  �� Binding �� *	Visible+ �� Value	 False.a���  � ��# [ ��  �� *	Visible+ True.a���  � ��# [ �� $��2��$��1q�
���   �._��� $
��Auto��$
��Auto������ *ImageBitmap+�� $��4��$��Center=�$
��Bold"����� *Name+�� $ layerLockedBtn��-   $��6��#��[ .���� ���� " �� ��  �� *Locked+	 False.a���  � ��# [ ��  �� *Locked+ True.a���  � ��# [ 