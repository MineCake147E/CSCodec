﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Filters.Prediction
{
    /// <summary>
    ///	Implementation of single-precision Linear Predictor.
    /// </summary>
    /// <seealso cref="Prediction.LinearPredictorBase{T}" />
    public sealed class LinearPredictorFloat : LinearPredictorBase<float>
    {
        /// <summary>
        /// The machine epsilon for <see cref="float"/>
        /// </summary>
        private static readonly float CEpsilon = BitConverter.ToSingle(new byte[] { 0, 0, 0, 0x34 }, 0);

        /// <summary>
        /// Initializes the Linear Predictor using specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="Order">The order of predictor.</param>
        /// <param name="errors">The errors.</param>
        public override void Initialize(ReadOnlySpan<float> data, int Order, out Span<float> errors)
        {
            Autocorrelation.CalculateAutocorrelation(data, out var acorr, Order, true);
            Coefficients = new float[Order];
            LevinsonDurbinRecursion(Coefficients.AsSpan(), acorr, Order, out errors);
        }

        /// <summary>
        /// Levinson durbin recursion calculation.
        /// </summary>
        /// <param name="coeff">The coeff.</param>
        /// <param name="acorr">The acorr.</param>
        /// <param name="order">The order.</param>
        /// <param name="errors">The errors.</param>
        private static void LevinsonDurbinRecursion(Span<float> coeff, ReadOnlySpan<float> acorr, int order, out Span<float> errors)
        {
            int delay, index;
            float lambda;
            errors = new float[order];
            if (Math.Abs(acorr[0]) < CEpsilon)  //Needs no Prediction
            {
                coeff.Fill(0);
                errors.Fill(0);
                return;
            }
            Span<float> VecA = stackalloc float[order + 2];
            Span<float> VecE = stackalloc float[order + 2];
            Span<float> VecU = stackalloc float[order + 2];
            Span<float> VecV = stackalloc float[order + 2];
            VecA.Fill(0);
            VecU.Fill(0);
            VecV.Fill(0);

            //First step
            VecA[0] = VecE[0] = VecU[0] = VecV[1] = 1f;
            VecA[1] = -acorr[1] / acorr[0];
            VecE[1] = acorr[0] + acorr[1] * VecA[1];

            //recursion
            for (delay = 1; delay < order; delay++)
            {
                lambda = 0;
                for (index = 0; index < delay + 1; index++)
                {
                    lambda += VecA[index] * acorr[delay + 1 - index];
                }
                lambda /= -VecE[delay];
                VecE[delay + 1] = (1 - lambda * lambda) * VecE[delay];

                //Update U, V
                for (index = 0; index < delay; index++)
                {
                    VecU[index + 1] = VecV[delay - index] = VecA[index + 1];
                }
                VecU[0] = 1;
                VecU[delay + 1] = 0;
                VecV[0] = 0;
                VecV[delay + 1] = 1;

                //Update Result
                for (index = 0; index < delay + 2; index++)
                {
                    VecA[index] = VecU[index] + lambda * VecV[index];
                }
            }
            VecA.Slice(1, order).CopyTo(coeff);
            VecE.Slice(1, order).CopyTo(errors);
            //Vectors get released.
        }

        /// <summary>
        /// Predicts outputs from the specified first data.
        /// </summary>
        /// <param name="first">The first data.</param>
        /// <param name="output">The prediction output without overlapping <paramref name="first" />. <paramref name="first" /> will be copied first.</param>
        public override void Predict(ReadOnlySpan<float> first, Span<float> output)
        {
            var predata = first.Slice(0, Math.Min(Order, first.Length));
            output.Slice(predata.Length).Fill(0);
            predata.CopyTo(output.Slice(0, predata.Length));
            for (int i = first.Length; i < output.Length; i++)
            {
                for (int delayIndex = 0; delayIndex < Order; delayIndex++)
                {
                    if (i - delayIndex - 1 < 0) continue;
                    output[i] -= Coefficients[delayIndex] * output[i - delayIndex - 1];
                }
            }
        }

        /// <summary>
        /// Resets this instance using specified coefficents.
        /// </summary>
        /// <param name="coefficents">The coefficents to be set.</param>
        public override void Reset(ReadOnlySpan<float> coefficents)
        {
            coefficents.CopyTo(Coefficients);
        }

        /// <summary>
        /// Resets this instance using specified global coefficient.
        /// </summary>
        /// <param name="fill">The coefficents to be set.</param>
        public override void Reset(float fill)
        {
            Coefficients.AsSpan().Fill(fill);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Coefficients = null;
                disposed = true;
            }
        }
    }
}
