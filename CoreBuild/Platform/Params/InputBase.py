#!/usr/bin/env python
# -*- coding: utf-8 -*-

from CoreBuild.Platform.Params.InputType import *

class InputBase(object):
    '''
    传入参数类型
    '''
    def __init__(self):
        self.value = None
        self.type = InputType.String
        self.maxValue = None
        self.minValue = None
        self.inputIndex = -1

    def checkValid(self):
        '''
        测试参数合法性
        :return:
        '''
        pass