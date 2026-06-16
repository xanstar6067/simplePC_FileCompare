using FileMerger.Models;
using System.Security.Cryptography;

namespace FileMerger.Services
{
    internal sealed class FileContentComparisonService
    {
        private const int BufferSize = 81920;

        public FileComparisonStatus Compare(
            FileSnapshot fileA,
            FileSnapshot fileB,
            FileComparisonOptions options,
            out string? errorMessage)
        {
            errorMessage = null;

            if (fileA.Size != fileB.Size)
            {
                return FileComparisonStatus.Different;
            }

            return options.VerifyMd5Checksum
                ? CompareMd5(fileA, fileB, out errorMessage)
                : CompareBytes(fileA, fileB, out errorMessage);
        }

        private static FileComparisonStatus CompareBytes(
            FileSnapshot fileA,
            FileSnapshot fileB,
            out string? errorMessage)
        {
            errorMessage = null;

            byte[] bufferA = new byte[BufferSize];
            byte[] bufferB = new byte[BufferSize];

            try
            {
                using FileStream streamA = File.OpenRead(fileA.FullPath);
                using FileStream streamB = File.OpenRead(fileB.FullPath);

                while (true)
                {
                    int readA = streamA.Read(bufferA, 0, bufferA.Length);
                    int readB = streamB.Read(bufferB, 0, bufferB.Length);

                    if (readA != readB)
                    {
                        return FileComparisonStatus.Different;
                    }

                    if (readA == 0)
                    {
                        return FileComparisonStatus.Same;
                    }

                    if (!bufferA.AsSpan(0, readA).SequenceEqual(bufferB.AsSpan(0, readB)))
                    {
                        return FileComparisonStatus.Different;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return FileComparisonStatus.ReadError;
            }
        }

        private static FileComparisonStatus CompareMd5(
            FileSnapshot fileA,
            FileSnapshot fileB,
            out string? errorMessage)
        {
            errorMessage = null;

            try
            {
                using FileStream streamA = File.OpenRead(fileA.FullPath);
                using FileStream streamB = File.OpenRead(fileB.FullPath);

                byte[] hashA = MD5.HashData(streamA);
                byte[] hashB = MD5.HashData(streamB);

                return hashA.AsSpan().SequenceEqual(hashB)
                    ? FileComparisonStatus.Same
                    : FileComparisonStatus.Different;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return FileComparisonStatus.ReadError;
            }
        }
    }
}
