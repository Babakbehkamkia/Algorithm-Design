# python3
import sys
class Node:
    def __init__(self,key,parent,edges):
        self.key=key
        self.parent=parent
        self.edges=edges
        self.is_ending=False

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __eq__(self,other):
        return self.value==other.value
NA = -1

# class Node:
# 	def __init__ (self):
# 		self.next = [NA] * 4


def f1(n,patterns):
    nodes=[]
    root=Node(0,-1,[])
    nodes.append(root)
    index=1
    for i in range(n):
        current_parent=root
        for j in range(len(patterns[i])):
            letter=patterns[i][j]
            node=Node(index,current_parent,[])
            
            edge=Edge(letter,current_parent,node)
            is_exist=False
            for item in current_parent.edges:
                if edge==item:
                    node=item.e
                    is_exist=True
            if not is_exist:
                current_parent.edges.append(edge)
                nodes.append(node)
                index+=1
            current_parent=node
        current_parent.is_ending=True
    return nodes

def f2(nodes,text):
    result=[]
    for i in range(len(text)):
        index=0
        j=i
        is_find=True
        while nodes[index].edges and j<len(text):
            is_exist=False
            for item in nodes[index].edges:
                if text[j]==item.value:
					# if len(nodes[item.e.key].edges)==0:
                    #     is_find=True
                    # if len(nodes[item.e.key].edges)==0 :
                    #     is_find=True
                    index=item.e.key
                    j+=1
					
					# is_find=True



                    is_exist=True
                    break
            if not is_exist:
                is_find=False
                break
        if is_find and nodes[index].is_ending:
            result.append(i)
    x=0
    return result
def solve (text, n, patterns):
	# result = []
	return f2(f1(n,patterns),text)
	# write your code here
	# result=[]	
    # x=0
    # return result

	# return result

text = sys.stdin.readline ().strip ()
n = int (sys.stdin.readline ().strip ())
patterns = []
for i in range (n):
	patterns += [sys.stdin.readline ().strip ()]

ans = solve (text, n, patterns)

sys.stdout.write (' '.join (map (str, ans)) + '\n')
