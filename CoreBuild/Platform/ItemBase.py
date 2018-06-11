#!/usr/bin/env python
# -*- coding: utf-8 -*-

from CoreBuild.Platform.Params.InputBase import *
from CoreBuild.Platform.Params.OutputBase import *
from CoreBuild.Platform.Params.InputType import *

import json

class ItemBase(object):
    def __init__(self):
        self.inputDict = {}
        self.outPut = None

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

    def self_to_json(self):
        return  json.dumps(self,default=lambda obj:obj.__dict__, sort_keys=True,indent=4)
        pass

if __name__ == "__main__":
    pass