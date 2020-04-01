import sys, os, yaml
from PyQt5.QtWidgets import QMainWindow, QApplication, QWidget, QInputDialog, QLineEdit, QFileDialog
from PyQt5 import QtCore, QtGui, QtWidgets, uic
from PyQt5.QtGui import QIcon
from Btm_temp import Ui_MainWindow




class mainwindow(QtWidgets.QMainWindow):
    aktuelles_Projekt = None
    aktuelles_Logbuch = None

    def __init__(self, parent = None):
        super(mainwindow, self).__init__(parent)
        self.ui = Ui_MainWindow()
        self.ui.setupUi(self)


        self.aktuelles_Projekt = None
        self.aktuelles_Logbuch = None



        self.ui.progressBar.setValue(0)


        self.ui.pushButton_NeuerEintrag.clicked.connect(self.Button_NeuerEintrag)
        self.ui.pushButton_EintragLoeschen.clicked.connect(self.Button_EintragLoeschen)
        
        self.ui.actionBerechnen.triggered.connect(self.berechnen)
        self.ui.actionLogbuch_speichern_unter.triggered.connect(self.Logbuch_speichern_unter)
        self.ui.actionNeues_Projekt.triggered.connect(self.Neues_Projekt)
        self.ui.actionLogbuch_speichen.triggered.connect(self.Logbuch_speichen)
        self.ui.actionProjekt_ffnen.triggered.connect(self.Projekt_ffnen)
        self.ui.actionLogbuch_laden.triggered.connect(self.Logbuch_laden)
        self.show()
    

    
    def Logbuch_speichern_unter(self):
        print("speichert Daten unter:")
        data = {}
        eintrag = 0
        column = 0
        logbuch = QFileDialog.getSaveFileName(self,"Logbuch speuchern unter", "","Logbuch (*.yaml)")
        self.ui.progressBar.setValue(10)
        if logbuch[0]:
            print(logbuch[0])
            self.ui.progressBar.setValue(10)
            self.aktuelles_Logbuch = logbuch[0]
            self.ui.label_pfad.setText(self.aktuelles_Logbuch)
            with open(logbuch[0], "w", encoding = "utf-8") as file:
                colomn_count = 0
                data['Info'] = {}
                data['Info']['Einheit'] = {}
                self.ui.progressBar.setValue(15)

                if self.ui.radioButton_gramm.isChecked():
                    data['Info']['Einheit'] = 'G'
                elif self.ui.radioButton_kilogramm.isChecked():
                    data['Info']['Einheit'] = 'KG'
                self.ui.progressBar.setValue(30)
                
                for row in range(self.ui.tableWidget.rowCount()):
                    Eintrag = str("Eintrag_"+ str(row))
                    data[Eintrag] = {}
                    column_count =0
                    data[Eintrag] = {'Lfd_Nr': "", 'Datum': "", 'Anfangsbestand': "", 'Theor_Zugang': "", 'Prakt_Zugang': "", 'Arbeitsverlust': "", 'Abgang': "", 'Aktueller_Bestand': "", 'Bemerkung': ""}
                    for column in data[Eintrag]:
                        #print("In Reihe ", row)
                        #print(column)
                        cell = self.ui.tableWidget.item(int(row), column_count)
                        if cell:
                            inf = cell.text()
                        else:
                            inf = " "
                        data[Eintrag][column] = inf
                        #print(inf)
                        column_count += 1
                self.ui.progressBar.setValue(90)
                data_nopoint = yaml.dump(data, file, sort_keys=False)
                #print(data)
                #print(yaml.dump(data, file, sort_keys=False))
                self.ui.progressBar.setValue(100)
                print(self.aktuelles_Logbuch+" wurde erfolgreich gespeichert")
                    
        else:
            print("speichern wurde vom Benutzer abgebrochen")
        self.ui.progressBar.setValue(0)   
    def Neues_Projekt(self):
        pass
    def Aktueller_Bestand(self, Anfangsbestand = 0.0, Praktischer_Zugang = 0.0, Arbeitsverlust = 0.0, Abgang = 0.0):
        AktuellerBestand = Anfangsbestand + Praktischer_Zugang - Arbeitsverlust - Abgang
        return AktuellerBestand
    # den "." durch ein "," ersetzen Ergebins nocmal nachprüfen

    def berechnen(self):
        row = None
        column_anf = 2
        column_prak = 4
        column_arbv= 5
        column_abga = 6
        column_aktb = 7
        Anfangsbestand = ""

        for row in range(self.ui.tableWidget.rowCount()):
            for column in range(self.ui.tableWidget.columnCount()):
                   cell = self.ui.tableWidget.item(row, column)
                   if cell:
                       pass
                   else:
                       self.ui.tableWidget.setItem(row, column, QtWidgets.QTableWidgetItem("0.0"))

        for row in range(self.ui.tableWidget.rowCount()):
           Anfangsbestand = self.Aktueller_Bestand(
               float((self.ui.tableWidget.item(row, column_anf)).text()), 
               float((self.ui.tableWidget.item(row, column_prak)).text()),
               float((self.ui.tableWidget.item(row, column_arbv)).text()),
               float((self.ui.tableWidget.item(row, column_abga)).text())
               )
           self.ui.tableWidget.setItem(row, column_aktb, QtWidgets.QTableWidgetItem(str(Anfangsbestand)))
           try:
               self.ui.tableWidget.setItem(row + 1, column_anf, QtWidgets.QTableWidgetItem(str(Anfangsbestand)))
           except:
               pass
            
        print("Tabelle wurde aktualisiert")
    def Logbuch_speichen(self):
        print("speichert Daten")
        data = {}
        eintrag = 0
        column = 0
        print(self.aktuelles_Logbuch)
        self.ui.progressBar.setValue(10)
        if self.aktuelles_Logbuch is None:
            self.Logbuch_speichern_unter()
            return
        with open(self.aktuelles_Logbuch, "w", encoding = "utf-8") as file:
                colomn_count = 0
                data['Info'] = {}
                data['Info']['Einheit'] = {}
                self.ui.progressBar.setValue(15)

                if self.ui.radioButton_gramm.isChecked():
                    data['Info']['Einheit'] = 'G'
                elif self.ui.radioButton_kilogramm.isChecked():
                    data['Info']['Einheit'] = 'KG'
                self.ui.progressBar.setValue(30)
                
                for row in range(self.ui.tableWidget.rowCount()):
                    Eintrag = str("Eintrag_"+ str(row))
                    data[Eintrag] = {}
                    column_count =0
                    data[Eintrag] = {'Lfd_Nr': "", 'Datum': "", 'Anfangsbestand': "", 'Theor_Zugang': "", 'Prakt_Zugang': "", 'Arbeitsverlust': "", 'Abgang': "", 'Aktueller_Bestand': "", 'Bemerkung': ""}
                    for column in data[Eintrag]:
                        #print("In Reihe ", row)
                        #print(column)
                        cell = self.ui.tableWidget.item(int(row), column_count)    
                        if cell:
                            inf = cell.text()
                        else:
                            inf = " "
                        data[Eintrag][column] = inf
                        #print(inf)
                        column_count += 1
                self.ui.progressBar.setValue(90)
                data_nopoint = yaml.dump(data, file, sort_keys=False)  
                self.ui.progressBar.setValue(100)
                print(self.aktuelles_Logbuch+" wurde erfolgreich gespeichert")
                self.ui.progressBar.setValue(0)
    #handeling bei falschen Datein... 
    def Logbuch_laden(self):
        print("lade Logbuch:")
        row = -1
        logbuch = QFileDialog.getOpenFileName(self,"Logbuch wählen", "","Logbuch (*.yaml)")
        if logbuch[0]:
            self.aktuelles_Logbuch = logbuch[0]
            self.ui.label_pfad.setText(self.aktuelles_Logbuch)
            self.ui.tableWidget.setRowCount(0)
            self.ui.progressBar.setValue(5)                    
            with open(self.aktuelles_Logbuch , 'r', encoding = "utf-8") as file:
                data = yaml.load(file, Loader=yaml.FullLoader)
                #print(inhalt)
                if data:
                    self.ui.progressBar.setValue(20)
                    if data['Info']['Einheit'] == "KG":
                        self.ui.radioButton_kilogramm.setChecked(True)
                        self.ui.radioButton_gramm.setChecked(False)
                    elif data['Info']['Einheit'] == "G":
                        self.ui.radioButton_kilogramm.setChecked(False)
                        self.ui.radioButton_gramm.setChecked(True)
                    else:
                        self.ui.radioButton_kilogramm.setChecked(False)
                        self.ui.radioButton_gramm.setChecked(False)
                    self.ui.tableWidget.setRowCount(len(data)-1)
                    self.ui.progressBar.setValue(30)
                    for eintrag, spalte in data.items():
                        column = 0
                        if eintrag == 'Info':
                            #print("info")
                            continue    
                        row +=1
                        for wert in spalte:
                            self.ui.tableWidget.setItem(row, column, QtWidgets.QTableWidgetItem(str(data[eintrag][wert])))
                            #print("item an",row,"\t",column)
                            column += 1
                            self.ui.progressBar.setValue(100)
                    self.ui.progressBar.setValue(0)
                    print(logbuch[0], "erfolgreich geladen")
        else:
            print("laden von Benutzer abgebrochen")
    def Projekt_ffnen(self):
        pass
    def Neues_Logbuch(self):
        self.Logbuch_speichen()
        self.ui.tableWidget.setRowCount(0)
        self.Button_NeuerEintrag()
        self.aktuelles_Logbuch = ""
        self.ui.label_pfad.setText(self.aktuelles_Logbuch)
    def Button_EintragLoeschen(self):
        self.ui.tableWidget.removeRow(self.ui.tableWidget.rowCount()-1)
    def Button_NeuerEintrag(self):
        self.ui.tableWidget.insertRow(self.ui.tableWidget.rowCount())

    pass
if __name__ == '__main__':
    app = QApplication(sys.argv)
    ex = mainwindow()
    ex.show()
    sys.exit(app.exec_())