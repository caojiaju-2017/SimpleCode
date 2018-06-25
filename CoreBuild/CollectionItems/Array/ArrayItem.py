#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Platform.ItemBase import *

import json

class ArrayItem(ItemBase):
    def __init__(self):
        super(ArrayItem, self).__init__()

        self.itemtype = ItemType.Collection
        self.shapetype = ImageShape.Image200001
        self.itemname = "数组"
        self.iteminfo = "模块实现了关闭某程序能力"
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
