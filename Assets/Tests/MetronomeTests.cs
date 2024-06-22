using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Src.MetronomeLogic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.TestTools;

public class MetronomeTests
{
    [Test]
    public void Metronome_test_1()
    {
        var sequence = new List<float> { 0.5f, 0.6f, 0.3f };
        var time = 5f;
        var metronome = new Metronome(sequence, 0.1f);
        var expectedMoments = new List<float> { 0.5f, 1.1f, 1.4f, 1.9f, 2.5f, 2.8f, 3.3f, 3.9f, 4.2f, 4.7f, 5f };
        
        var actualMoments = metronome.GetBeatsMomentsForNextInterval(time);

        for (int i = 0; i < actualMoments.Count; i++)
        {
            var actualMoment = actualMoments[i];
            var expectedMoment = expectedMoments[i];
            Assert.AreEqual(expectedMoment, actualMoment, 0.01f);
        }
    }
    
    [Test]
    public void Metronome_test_2()
    {
        var sequence = new List<float> { 0.5f, 0.6f, 0.3f };
        var time = 5f;
        var metronome = new Metronome(sequence, 0.1f);
        var expectedMoments = new List<float> { 0.4f, 1.0f, 1.3f, 1.8f, 2.4f, 2.7f, 3.2f, 3.8f, 4.1f, 4.6f, 4.9f };
        
        metronome.Update(0.1f);
        var actualMoments = metronome.GetBeatsMomentsForNextInterval(time);

        for (int i = 0; i < actualMoments.Count; i++)
        {
            var actualMoment = actualMoments[i];
            var expectedMoment = expectedMoments[i];
            Assert.AreEqual(expectedMoment, actualMoment, 0.01f);
        }
    }
    
    [Test]
    public void Metronome_test_3()
    {
        var sequence = new List<float> { 0.5f, 0.6f, 0.3f };
        var time = 5f;
        var metronome = new Metronome(sequence, 0.1f);
        var expectedMoments = new List<float> { 0.5f, 0.8f, 1.3f, 1.9f, 2.2f, 2.7f, 3.3f, 3.6f, 4.1f, 4.7f };
        
        metronome.Update(0.1f);
        metronome.Update(0.5f);
        var actualMoments = metronome.GetBeatsMomentsForNextInterval(time);

        for (int i = 0; i < actualMoments.Count; i++)
        {
            var actualMoment = actualMoments[i];
            var expectedMoment = expectedMoments[i];
            Assert.AreEqual(expectedMoment, actualMoment, 0.01f);
        }
    }

    [Test]
    public void Metronome_Deltas_Test()
    {
        var sequence = new List<float> { 0.5f, 0.6f, 0.3f };
        var metronome = new Metronome(sequence, 0.1f);
        var updateTime = 0.55f;
        var expectedDeltas = new List<float> { 0.55f, 0.85f };

        metronome.Update(updateTime);
        var actualDeltas = metronome.GetCurrentDeltas();

        for (int i = 0; i < actualDeltas.Count; i++)
        {
            var expectedDelta = expectedDeltas[i];
            var actualDelta = actualDeltas[i];
            Assert.AreEqual(expectedDelta, actualDelta, 0.01f);
        }
    }

    [Test]
    public void Metronome_Deltas_Test_2()
    {
        var sequence = new List<float> { 0.5f, 0.6f, 0.3f };
        var metronome = new Metronome(sequence, 0.1f);
        var updateTime = 1.5f;
        var expectedDeltas = new List<float> {0.4f, 1.0f, 1.3f };

        metronome.Update(updateTime);
        var actualDeltas = metronome.GetCurrentDeltas();

        for (int i = 0; i < actualDeltas.Count; i++)
        {
            var expectedDelta = expectedDeltas[i];
            var actualDelta = actualDeltas[i];
            Assert.AreEqual(expectedDelta, actualDelta, 0.01f);
        }
    }
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MetronomeTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
