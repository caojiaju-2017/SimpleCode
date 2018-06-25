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
        self.itemtype = ItemType.Module
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


class ImageShape(object):
    Image100001 = 100001
    Image100002 = 100002
    Image100003 = 100003
    Image100004 = 100004
    Image100005 = 100005
    Image100006 = 100006
    Image100007 = 100007
    Image100008 = 100008
    Image100009 = 100009
    Image100010 = 100010
    Image100011 = 100011
    Image100012 = 100012
    Image100013 = 100013
    Image100014 = 100014
    Image100015 = 100015
    Image100016 = 100016
    Image100017 = 100017
    Image100018 = 100018
    Image100019 = 100019
    Image100020 = 100020

    Image200001 = 200001
    Image200002 = 200002
    Image200003 = 200003
    Image200004 = 200004
    Image200005 = 200005
    Image200006 = 200006
    Image200007 = 200007
    Image200008 = 200008
    Image200009 = 200009
    Image200010 = 200010

    Image300001 = 300001
    Image300002 = 300002
    Image300003 = 300003
    Image300004 = 300004
    Image300005 = 300005
    Image300006 = 300006
    Image300007 = 300007
    Image300008 = 300008
    Image300009 = 300009
    Image300010 = 300010


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
    Module = 902
    Collection = 903

if __name__ == "__main__":
    pass