# import demjson
import json


class te():
    def __init__(self):
        self.name = 'aaa'
        self.sex = 'man'


t = te()

print(t)
# json1 = demjson.encode(t, default=lambda obj: obj.__dict__)
json1 = json.dumps(t, default=lambda obj: obj.__dict__, indent=4)

print(json1)

t2 = json.loads(json1)

print('-----')
print(t2["name"])
print('**************')
s = "sdfsdfsfsf"

json2 = json.dumps(s)
print(json2)
