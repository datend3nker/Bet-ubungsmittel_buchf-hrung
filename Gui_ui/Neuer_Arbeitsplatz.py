# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'Neuer_Arbeitsplatz.ui'
#
# Created by: PyQt5 UI code generator 5.13.2
#
# WARNING! All changes made in this file will be lost!


from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_Neuer_Arbeitsplatz(object):
    def setupUi(self, Neuer_Arbeitsplatz):
        Neuer_Arbeitsplatz.setObjectName("Neuer_Arbeitsplatz")
        Neuer_Arbeitsplatz.resize(491, 223)
        self.gridLayout = QtWidgets.QGridLayout(Neuer_Arbeitsplatz)
        self.gridLayout.setObjectName("gridLayout")
        self.label_4 = QtWidgets.QLabel(Neuer_Arbeitsplatz)
        font = QtGui.QFont()
        font.setPointSize(11)
        self.label_4.setFont(font)
        self.label_4.setObjectName("label_4")
        self.gridLayout.addWidget(self.label_4, 0, 0, 1, 1)
        self.dateEdit = QtWidgets.QDateEdit(Neuer_Arbeitsplatz)
        font = QtGui.QFont()
        font.setPointSize(11)
        self.dateEdit.setFont(font)
        self.dateEdit.setObjectName("dateEdit")
        self.gridLayout.addWidget(self.dateEdit, 0, 1, 1, 2)
        self.label_2 = QtWidgets.QLabel(Neuer_Arbeitsplatz)
        font = QtGui.QFont()
        font.setPointSize(11)
        self.label_2.setFont(font)
        self.label_2.setObjectName("label_2")
        self.gridLayout.addWidget(self.label_2, 1, 0, 1, 1)
        self.lineEdit = QtWidgets.QLineEdit(Neuer_Arbeitsplatz)
        self.lineEdit.setObjectName("lineEdit")
        self.gridLayout.addWidget(self.lineEdit, 1, 1, 1, 2)
        self.label = QtWidgets.QLabel(Neuer_Arbeitsplatz)
        font = QtGui.QFont()
        font.setPointSize(11)
        self.label.setFont(font)
        self.label.setObjectName("label")
        self.gridLayout.addWidget(self.label, 2, 0, 1, 1)
        self.lineEdit_2 = QtWidgets.QLineEdit(Neuer_Arbeitsplatz)
        self.lineEdit_2.setObjectName("lineEdit_2")
        self.gridLayout.addWidget(self.lineEdit_2, 2, 1, 1, 1)
        self.toolButton = QtWidgets.QToolButton(Neuer_Arbeitsplatz)
        self.toolButton.setObjectName("toolButton")
        self.gridLayout.addWidget(self.toolButton, 2, 2, 1, 1)
        self.label_3 = QtWidgets.QLabel(Neuer_Arbeitsplatz)
        self.label_3.setObjectName("label_3")
        self.gridLayout.addWidget(self.label_3, 3, 0, 1, 1)
        self.plainTextEdit = QtWidgets.QPlainTextEdit(Neuer_Arbeitsplatz)
        self.plainTextEdit.setObjectName("plainTextEdit")
        self.gridLayout.addWidget(self.plainTextEdit, 4, 0, 1, 3)
        self.pushButton = QtWidgets.QPushButton(Neuer_Arbeitsplatz)
        self.pushButton.setObjectName("pushButton")
        self.gridLayout.addWidget(self.pushButton, 5, 0, 1, 1)
        self.pushButton_2 = QtWidgets.QPushButton(Neuer_Arbeitsplatz)
        self.pushButton_2.setObjectName("pushButton_2")
        self.gridLayout.addWidget(self.pushButton_2, 5, 1, 1, 2)

        self.retranslateUi(Neuer_Arbeitsplatz)
        QtCore.QMetaObject.connectSlotsByName(Neuer_Arbeitsplatz)

    def retranslateUi(self, Neuer_Arbeitsplatz):
        _translate = QtCore.QCoreApplication.translate
        Neuer_Arbeitsplatz.setWindowTitle(_translate("Neuer_Arbeitsplatz", "Neuer Arbeitsplatz"))
        self.label_4.setText(_translate("Neuer_Arbeitsplatz", "Datum"))
        self.label_2.setText(_translate("Neuer_Arbeitsplatz", "Name"))
        self.label.setText(_translate("Neuer_Arbeitsplatz", "Pfad"))
        self.toolButton.setText(_translate("Neuer_Arbeitsplatz", "..."))
        self.label_3.setText(_translate("Neuer_Arbeitsplatz", "Bemerkung"))
        self.pushButton.setText(_translate("Neuer_Arbeitsplatz", "OK"))
        self.pushButton_2.setText(_translate("Neuer_Arbeitsplatz", "Abbrechen"))
