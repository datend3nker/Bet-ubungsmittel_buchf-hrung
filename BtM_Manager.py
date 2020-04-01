import sys, os, yaml
from PyQt5.QtWidgets import QMainWindow, QApplication, QWidget, QInputDialog, QLineEdit, QFileDialog, QDialog
from PyQt5 import QtCore, QtGui, QtWidgets, uic
from PyQt5.QtGui import QIcon
from BtM_gui import Ui_MainWindow


class MainWindow(QMainWindow):
    def __init__(self):
        super(MainWindow, self).__init__()
        ui = uic.loadUi('Gui_ui//BtM_gui.ui', self)
        self.show()

        self.pushButton.clicked.connect(self.abd)
        
        
    def abd(self):
        self.testhv = NeuerArbeitsplatzWindow()
        self.testhv.show()
        self.hide()

class NeuerArbeitsplatzWindow(QDialog):
    Pfad = ''
    def __init__(self):
        super(NeuerArbeitsplatzWindow, self).__init__()
        ui = uic.loadUi('Gui_ui//Neuer_Arbeitsplatz.ui', self)
        


 
class test(QWidget):
    Pfad = ''
    def __init__(self):
        super(test, self).__init__()
        ui = uic.loadUi('Gui_ui//test.ui', self)
        
               



if __name__ == '__main__':
    app = QApplication(sys.argv)
    ex = MainWindow()
    sys.exit(app.exec_())