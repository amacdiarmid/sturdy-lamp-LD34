using UnityEngine;
using System.Collections;

public class CreepList  {

    CreepAI First = null, Last = null;

    public void push( CreepAI c ) {
        if(c.Next != null) Debug.LogError("err");
        c.Next = Last;
        if(Last) Last.Prev = c;
        else First = c;
        Last = c;
    }

    public void remove( CreepAI c ) {
        if(c == First) First = c.Prev;
        else c.Next.Prev = c.Prev;
        if(c == Last) Last = c.Next;
        else c.Prev.Next = c.Next;
    }

    public void swap( CreepAI a, CreepAI b ) {

        if(a.Next == null) First = b;
        else a.Next.Prev = b;
        if(a.Prev == null) Last = b;
        else a.Prev.Next = b;

        if(b.Next == null) First = a;
        else b.Next.Prev = a;
        if(b.Prev == null) Last = a;
        else b.Prev.Next = a;

        CreepAI n = a.Next, p = a.Prev;
        a.Next = b.Next; a.Prev = b.Prev;
        b.Next = n; b.Prev = p;

    }
}
