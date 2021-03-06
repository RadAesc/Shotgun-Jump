﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SmallSpider
    :
    IsOnScreen
{
    void Awake()
    {
        dir = GetRandDir();
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Assert.IsNotNull( body );

        if( IsActivated() )
        {
            jumpTimer.Update( Time.deltaTime );

            if( jumpTimer.IsDone() && willJump )
            {
                body.AddForce( new
                    Vector2( 0.0f,jumpPower ),
                    ForceMode2D.Impulse );

                jumpTimer.Reset();
            }
            willJump = false;

            body.AddForce( new
                Vector2( ( float )dir * speed,0.0f ) *
                Time.deltaTime * dtOffset );

            Vector2 tPos = ( Vector2 )transform.position;
            if( tPos == lastPos &&
                tPos == posBeforeThat )
            {
                // print( "I'm stuck!!" );
                dir = GetRandDir();

                posBeforeThat.Set( -9999.0f,-9999.0f );
            }
            else
            {
                posBeforeThat = lastPos;
                lastPos = ( Vector2 )transform.position;
            }
        }
    }
    void OnCollisionExit2D( Collision2D coll )
    {
        // dir = GetRandDir();
        willJump = true;
    }
    int GetRandDir()
    {
        return( ( Random.Range( 0,10 ) > 5 ) ? 1 : -1 );
    }
    public void Jump()
    {
        Assert.IsNotNull( body );
        body.AddForce( GetForceDir(),ForceMode2D.Impulse );
    }
    Vector2 GetForceDir()
    {
        float randX = Random.Range( -2.1f,2.1f );
        float randY = Random.Range( 5.1f,12.1f );

        return ( new Vector2( randX,randY ) );
    }
    // 
    Rigidbody2D body;
    int dir = -1;
    const float speed = 10.0f;
    const float jumpPower = 4.61542f;
    Timer jumpTimer = new Timer( 2.15f );
    Vector2 lastPos = new Vector2( 0.0f,0.0f );
    Vector2 posBeforeThat = new Vector2( 0.0f,0.0f );
    const float dtOffset = 1.0f / 0.01700295f;
    bool willJump = false;
}
