   M S B A M L   `   `   `  ����E  AXNAControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:XNAControl;assembly=XNAControl
XNAControl  5   	   D @MapEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MapEditor.UserControlsMapEditor.UserControls 5   	      MapEditor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iXceed.Wpf.AvalonDock.Themes.Expression, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h	 dXceed.Wpf.AvalonDock.Themes.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4u
 qXceed.Wpf.AvalonDock.Themes.Office2007, Version=2.6.15111.10510, Culture=neutral, PublicKeyToken=ba83ff368b7563c6v rXceed.Wpf.AvalonDock.Themes.MetroAccent, Version=2.6.15111.10510, Culture=neutral, PublicKeyToken=ba83ff368b7563c6[ WXceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4Eavalon,http://schemas.xceed.com/wpf/xaml/avalondock    	 
   5   	   I
xnaControl,clr-namespace:XNAControl;assembly=XNAControl    	 
   5   	   ?controls$clr-namespace:MapEditor.UserControls    	 
   5   	   -   5          � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)    � 	Resources 5
       �� Source�� 5   
   $! /Themes/ExpressionDark.xaml?�6   %    .��5      � -   5        $
��root��5         NormalBrush#��[  6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��* �6   .+�5   
   c� -   5        $��toolBar��5      $��1��6!   o� 5   
   -   5        $
��menu��5      .@�5      m� -   5        $��fileMenuItem��5   %   $
��File��6   .@�5      m� -   5        $��newProjectMenuItem��5   0   $��New project��6   6J   m� 5      -   5        $��loadProjectMenuItem��5   1   $��Load project��6   6L   m� 5      -   5        $��saveProjectMenuItem��5   1   $��Save project��6   6L   �� 5      m� 5      -	   5        $��saveAsImageMenuItem��5   2   $��Save as image��6   62   �� 5      m� 5      -
   5        $��exitProgramMenuItem��5   )   $
��Exit��6   6)   5       m� 5!      -   5        $��viewMenuItem��5!   %   $
��View��6   .@�5"      m� -   5        $��windowsMenuItem��5"   ,   $��Windows��6   .@�5#      m� -   5        $��viewMapMenuItem��5#   ,   $	��Map��6   6C   m� 5$      -   5        $��viewPropertiesMenuItem��5$   3   $��
Properties��6   6Q   m� 5%      -   5        $!��viewProjectExplorerMenuItem��5%   9   $��Project explorer��6   6\   5&      �� 5'      m� 5(      $��	Show grid��6   m� 5)      $��Show objects��6   m� 5*      $��Show collision bodies��6   5+      m� 5,      -   5        $��editMenuItem��5,   %   $
��Edit��6   .@�5-      m� -   5        $��copyMenuItem��5-   )   $
��Copy��6   6)   m� 5.      -   5        $��pasteMenuItem��5.   )   $
��Past��6   6)   m� 5/      -   5        $��cutMenuItem��5/   (   $	��Cut��6   6(   �� 50      m� 51      -   5        $��undoMenuItem��51   )   $
��Undo��6   6)   m� 52      -   5        $��redoMenuItem��52   )   $
��Redo��6   6)   53      m� 54      -   5        $��addMenuItem��54   $   $	��Add��6   6$   m� 55      -   5        $��helpMenuItem��55   %   $
��Help��6   6%   56      )  #Xceed.Wpf.AvalonDock.DockingManager  58   
   -   5          Name$ dockingManager��58       $��2��68      Layout. 59      ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot     	RootPanel. 5:      -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel     Orientation$ 
Horizontal=�6%   �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5;      6  0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  �  �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]	   	DockWidth$		 200 �62   �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]
   Children.
 5<      2  ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable  -   5        /	  )Xceed.Wpf.AvalonDock.Layout.LayoutContent 	  Title$ Project explorer��5<   2   projectExplorerView�� 	  Content. 5=      �� -   5        $��projectExplorerRoot��5=   %   #��[  6@   5>      5?        5A      $		 880 �62   .
 5B        -   5        $	 Map��5B   2   mapView��. 5C      
   XNAControl.UserControl1
  -   5         
 Name$ 
xnaControl��5C   6   6J   5D      5E        5G         DockMinWidth$	 200Q�62   .
 5H        -   5        $ 
Properties��5H   2   propertiesView��. 5I      �� #��[  6)   5J      5K      5L      5M      5N      5O      5P      