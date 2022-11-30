# python3
n, m = map(int, input().split())
clauses = [ list(map(int, input().split())) for i in range(m) ]


def explore_inverse(adj,visited,index,post):
    for i in range(len(adj[index])):
        if visited[adj[index][i]-1]==False:
            visited[adj[index][i]-1]=True
            explore_inverse(adj,visited,adj[index][i],post)
    post.append(index)

def explore(adj,visited,index,new_scc,l,count):
    for i in range(len(adj[index])):
        if visited[adj[index][i]-1]==False:
            visited[adj[index][i]-1]=True
            new_scc.append(adj[index][i])
            l[adj[index][i]-1]=count
            explore(adj,visited,adj[index][i],new_scc,l,count)


def strongly_connected_components(n,adj,adj_reverse):
    l=[-1 for i in range(2*n)]
    count=0
    visited=[False for i in range(1,2*n+1)]
    visited_inverse=[False for i in range(1,2*n+1)]
    post=[]
    for i in range(1,2*n+1):
        if visited_inverse[i-1]==False:
            visited_inverse[i-1]=True
            explore_inverse(adj_reverse,visited_inverse,i,post)
    post.reverse()
    index=0
    scc=[]
    while index<len(post):
        new_scc=[]
        if visited[post[index]-1]==False:
            visited[post[index]-1]=True
            new_scc.append(post[index])
            l[post[index]-1]=count
            explore(adj,visited,post[index],new_scc,l,count)
        index+=1
        scc.append(new_scc)
        count+=1
    return scc,l

def make_adj(n,clauses):
    adj=[[] for i in range(2 * n + 1)]
    adj_inverse=[[] for i in range(2 * n + 1)]
    for item in clauses:
        if len(item)==1:
            if item[0]<0:
                t=-item[0]
                adj[t].append(t+n)
                adj_inverse[t+n].append(t)
            else:
                adj[item[0]+n].append(item[0])
                adj_inverse[item[0]].append(item[0]+n)
        else:
            if item[0]<0 and item[1]>0:
                t=-item[0]
                adj[t].append(item[1])
                adj[item[1]+n].append(t+n)
                adj_inverse[item[1]].append(t)
                adj_inverse[t+n].append(item[1]+n)
            elif item[0]>0 and item[1]<0:
                t=-item[1]
                adj[item[0]+n].append(t+n)
                adj[t].append(item[0])
                adj_inverse[t+n].append(item[0]+n)
                adj_inverse[item[0]].append(t)
            elif item[0]<0 and item[1]<0:
                t0=-item[0]
                t1=-item[1]
                adj[t0].append(t1+n)
                adj[t1].append(t0+n)
                adj_inverse[t1+n].append(t0)
                adj_inverse[t0+n].append(t1)
            else:
                adj[item[0]+n].append(item[1])
                adj[item[1]+n].append(item[0])
                adj_inverse[item[1]].append(item[0]+n)
                adj_inverse[item[0]].append(item[1]+n)
    return adj,adj_inverse

    
# This solution tries all possible 2^n variable assignments.
# It is too slow to pass the problem.
# Implement a more efficient algorithm here.
def isSatisfiable(n,clauses):
    adj,adj_inverse=make_adj(n,clauses)
    scc,var_to_scc=strongly_connected_components(n,adj,adj_inverse)
    for i in range(n):
        if var_to_scc[i]==var_to_scc[i+n]:
            return None
    
    values=[-1 for i in range(2*n)]
    
    for i in range(len(scc)-1,-1,-1):
        one_found=False
        zero_found=False
        for j in range(len(scc[i])):
            if values[scc[i][j]-1]==0:
                zero_found=True
            if values[scc[i][j]-1]==1:
                one_found=True
            if one_found and zero_found:
                return None
                

        if not one_found and not zero_found:
            for j in range(len(scc[i])):
                for item in adj_inverse[scc[i][j]]:
                    if values[item-1]==1:
                        one_found=True
                        break
                if one_found:
                    break
        if not one_found:
            # scc_group[i]=0
            for j in range(len(scc[i])):
                if values[scc[i][j]-1]==-1:
                    for item in adj_inverse[scc[i][j]]:
                        # if scc_group(var_to_scc[item])==1:
                        if values[item-1]==1:
                            return None
                    if scc[i][j]<=n:
                        values[scc[i][j]-1]=0
                        values[scc[i][j]+n-1]=1
                    else:
                        values[scc[i][j]-1]=0
                        values[scc[i][j]-n-1]=1
        else:
            # scc_group[i]=1
            for j in range(len(scc[i])):
                # if values[scc[i][j]-1]==0 and len(adj_inverse[scc[i][j]])!=0:
                #     return None
                if values[scc[i][j]-1]==-1:
                    # for item in adj_inverse[scc[i][j]]:
                    #     if scc_group(var_to_scc[item])==0:
                    #         return None
                    if scc[i][j]<=n:
                        values[scc[i][j]-1]=1
                        values[scc[i][j]+n-1]=0

                    else:
                        values[scc[i][j]-1]=1
                        values[scc[i][j]-n-1]=0

    result=[]

    for i in range(n):
        if values[i]==1:
            result.append(i+1)
        else:
            result.append(-(i+1))
    return result






    

result = isSatisfiable(n,clauses)
if result is None:
    print("UNSATISFIABLE")
else:
    print("SATISFIABLE")
    print(" ".join(str(result[i]) for i in range(n)))
