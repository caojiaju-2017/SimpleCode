#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Platform.ItemBase import *

import json

class SnapItem(ItemBase):
    def __init__(self):
        super(SnapItem, self).__init__()

        self.itemtype = ItemType.Module
        self.shapetype = ImageShape.Image100015
        self.itemname = "截图"
        self.iteminfo = "Python 类可以定义专用方法,专用方法是在特殊情况下或当使用特别语法时由 Python 替你调用的，而不是在代码中直接调用（象普通的方法那样）"
        self.buildConfig()
        pass

    def getCfgJson(self):
        return  self.self_to_json()

    def checkResult(self):
        print("sub class")
    def buildConfig(self):
        '''
        接口初始化函数
        :return:
        '''
        inputS1 = InputBase()
        inputS1.type = InputType.String
        self.setInputDefine("param1", inputS1)

        inputS2 = InputBase()
        inputS2.type = InputType.String
        inputS2.inputIndex = 2
        self.setInputDefine("param2", inputS2)

        outputS = OutputBase()
        outputS.type = InputType.Boolean
        self.setOutput(outputS)
