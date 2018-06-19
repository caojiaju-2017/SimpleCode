#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Platform.Params.InputBase import *
from Platform.Params.OutputBase import *
from Platform.Params.InputType import *

import json

class ItemBase(object):
    def __init__(self):
        self.inputDict = {}
        self.outPut = None
        self.size = [0,0]
        self.location = (0,0)
        self.shapetype = ItemShap.Circle
        self.itemtype = ItemType.Base
        self.itemname = None
        self.iteminfo = "Python 类可以定义专用方法,专用方法是在特殊情况下或当使用特别语法时由 Python 替你调用的，而不是在代码中直接调用（象普通的方法那样）"
    def setInputDefine(self,name ,paramS):
        '''
        设置输入参数定义
        :return:
        '''
        # 空参数
        if not paramS:
            return

        # 如果未指定 顺序
        if paramS.inputIndex == -1:
            paramS.inputIndex = len(self.inputDict)
            pass

        self.inputDict[name] = paramS
        pass

    def setOutput(self,value):
        '''
        设置输出参数定义
        :param value:
        :return:
        '''

        self.outPut = value
        pass
    def getOutput(self):
        return

    def checkResult(self):
        print("super class")
        return

    def self_to_json(self):
        return  json.dumps(self,default=lambda obj:obj.__dict__, sort_keys=True,indent=4)
        pass

class ItemShap(object):
    Triangle0 = 10001
    Triangle1 = 10002
    Circle    = 10003
    Square    = 10004
    Rectangle0= 10005
    Rectangle1= 10006
    Trapezoid0 = 10007
    Trapezoid1 = 10008
    Parallelogram0 = 10009
    Parallelogram1 = 10010

class ItemType(object):
    Control = 901
    Base = 902
    Collection = 903

if __name__ == "__main__":
    pass