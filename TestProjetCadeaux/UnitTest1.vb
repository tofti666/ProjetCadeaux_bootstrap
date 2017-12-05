Imports System.Text

<TestClass()>
Public Class UnitTest1

    Private testContextInstance As TestContext

    '''<summary>
    '''Obtient ou définit le contexte de test qui fournit
    '''des informations sur la série de tests active ainsi que ses fonctionnalités.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Attributs de tests supplémentaires"
    '
    ' Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
    '
    ' Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()>
    Public Sub TestMethod1()
        ' TODO: ajoutez ici la logique du test
    End Sub

End Class
