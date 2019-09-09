<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'Username'" />
        <xsl:with-param name="firstValue" select="@subacc" />

        <xsl:with-param name="secondKey" select="'SelectAll'" />
        <xsl:with-param name="secondValue" select="@selectall" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'AddedAccount'" />
        <xsl:with-param name="firstValue" select="@addedaccount" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'RemovedAccount'" />
        <xsl:with-param name="firstValue" select="@removedaccount" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'IP'" />
        <xsl:with-param name="firstValue" select="xslLogFormat:FormatIP(@loginip)" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>