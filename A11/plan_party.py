#uses python3

import sys
import threading

# This code is used to avoid stack overflow issues
sys.setrecursionlimit(10**6) # max depth of recursion
threading.stack_size(2**26)  # new thread will get stack of such size


class Vertex:
    def __init__(self, weight):
        self.weight = weight
        self.children = []


def ReadTree():
    size = int(input())
    tree = [Vertex(w) for w in map(int, input().split())]
    for i in range(1, size):
        a, b = list(map(int, input().split()))
        tree[a - 1].children.append(b - 1)
        tree[b - 1].children.append(a - 1)
    return tree


def dfs(tree, vertex, parent,ranks,rank):
    if len(tree[vertex].children):
        for child in tree[vertex].children:
            if child != parent:
                ranks[child]=rank+1
                dfs(tree, child, vertex,ranks,rank+1)

    # This is a template function for processing a tree using depth-first search.
    # Write your code here.
    # You may need to add more parameters to this function for child processing.


def MaxWeightIndependentTreeSubset(tree):
    size = len(tree)
    if size == 0:
        return 0
    ranks=[-1 for i in range(size)]
    ranks[0]=0
    dfs(tree, 0, -1,ranks,0)
    # You must decide what to return.
    m=max(ranks)
    l=[[] for i in range(m+1)]
    for i in range(len(ranks)):
        l[ranks[i]].append(i)

    for i in range(len(l)-2,-1,-1):
        for j in l[i]:
            sum=0
            sum2=tree[j].weight
            for k in tree[j].children:
                if ranks[j]<ranks[k]:
                    sum+=tree[k].weight
                    for c in tree[k].children:
                        if ranks[k]<ranks[c]:
                            sum2+=tree[c].weight
            tree[j].weight=max(sum,sum2)

    return tree[0].weight


def main():
    tree = ReadTree()
    weight = MaxWeightIndependentTreeSubset(tree)
    print(weight)


# This is to avoid stack overflow issues
threading.Thread(target=main).start()
